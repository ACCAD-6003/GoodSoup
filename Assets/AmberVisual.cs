using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class AmberVisual : MonoBehaviour
{
    public enum HairOption { BONNET, MESSY, CLEAN }
    public enum FaceOption { HAPPY, PARANOID, SAD, GROGGY, NEUTRAL }
    Dictionary<FaceOption, string> faceToTexture = new() {
        { FaceOption.HAPPY, "Happy" },
        { FaceOption.PARANOID, "Paranoid"},
        { FaceOption.SAD, "Sad"},
        { FaceOption.GROGGY, "Groggy"},
        { FaceOption.NEUTRAL, "Neutral"}
    };
    [SerializeField] GameObject _backpack;
    [SerializeField] GameObject _chefHat;
    [SerializeField] GameObject _towel;
    [SerializeField] Material _amberBodyMaterial;
    [SerializeField] Material _amberFaceMaterial;
    [SerializeField] Material _amberDoomGuyMaterial;

    [SerializeField] GameObject _bonnet;
    [SerializeField] GameObject _hairMessy;
    [SerializeField] GameObject _hairBraided;

    private void OnEnable()
    {
        // Disable anything that doesn't need to be shown (i.e. backpack was enabled in editor to see how it looks)
        _backpack.SetActive(false);

        // Set things to correspond with storydata on Awake (first value is a dummy value)
        UpdateBackpackVisual(false, StoryDatastore.Instance.PickedUpBackpack.Value);
        // An important distinction -- AmberWornClothing differs from ChosenClothing
        UpdateClothesVisual(ClothingOption.Dirty, StoryDatastore.Instance.AmberWornClothing.Value);
        // Update hair
        UpdateHairVisual(HairOption.BONNET, StoryDatastore.Instance.AmberHairOption.Value);

        UpdateChefHatVisual(false, StoryDatastore.Instance.WearingChefHat.Value);

        // Subscribe to changes in storydata for each visual
        StoryDatastore.Instance.PickedUpBackpack.Changed += UpdateBackpackVisual;
        StoryDatastore.Instance.AmberWornClothing.Changed += UpdateClothesVisual;
        StoryDatastore.Instance.AmberHairOption.Changed += UpdateHairVisual;
        StoryDatastore.Instance.WearingChefHat.Changed += UpdateChefHatVisual;

        StoryDatastore.Instance.Paranoia.Changed += SetAmberMood;
        StoryDatastore.Instance.Annoyance.Changed += SetAmberMood;
        StoryDatastore.Instance.Happiness.Changed += SetAmberMood;
        StoryDatastore.Instance.FaceOption.Changed += UpdateFaceVisual;
    }
    
    private void OnDisable()
    {
        // Subscribe to changes in storydata for each visual
        StoryDatastore.Instance.PickedUpBackpack.Changed -= UpdateBackpackVisual;
        StoryDatastore.Instance.AmberWornClothing.Changed -= UpdateClothesVisual;
        StoryDatastore.Instance.AmberHairOption.Changed -= UpdateHairVisual;
    }
    void SetAmberMood(float oldValue, float newValue) {
        Dictionary<StoryData<float>, FaceOption> moods = new()
        {
            { StoryDatastore.Instance.Happiness, FaceOption.HAPPY },
            { StoryDatastore.Instance.Annoyance, FaceOption.SAD },
            { StoryDatastore.Instance.Paranoia, FaceOption.PARANOID }
        };
        float maxValue = 0f;
        FaceOption faceOption = FaceOption.NEUTRAL;
        foreach (var moodIntensity in moods) {
            if (moodIntensity.Key.Value > maxValue) { 
                maxValue = moodIntensity.Key.Value;
                faceOption = moodIntensity.Value;
            }
        }
        StoryDatastore.Instance.FaceOption.Value = faceOption;
    }
    void UpdateFaceVisual(FaceOption _, FaceOption newFace) {
        Texture2D text = Resources.Load<Texture2D>("Textures/FacialExpressions/" + StoryDatastore.Instance.);
        _amberBodyMaterial.SetTexture("_Albedo", text);
    }
    void UpdateBackpackVisual(bool oldValue, bool newValue) {
        _backpack.SetActive(newValue);
    }
    
    void UpdateChefHatVisual(bool oldValue, bool newValue)
    {
        _chefHat.SetActive(newValue);
    }
    void UpdateClothesVisual(ClothingOption oldValue, ClothingOption newValue) {
        _towel.SetActive(newValue == ClothingOption.Towel);
        Texture2D text = Resources.Load<Texture2D>("Textures/Clothing/" + Dresser.ClothingOptions[newValue]);
        _amberBodyMaterial.SetTexture("_Albedo", text);
    }

    void UpdateHairVisual(HairOption oldValue, HairOption newValue) {
        _bonnet.SetActive(newValue == HairOption.BONNET);
        _hairMessy.SetActive(newValue == HairOption.MESSY);
        _hairBraided.SetActive(newValue == HairOption.CLEAN);
    }
}
