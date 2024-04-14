using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberVisual : MonoBehaviour
{
    public enum HairOption { BONNET, MESSY, CLEAN }
    [SerializeField] GameObject _backpack;
    private void Awake()
    {
        // Disable anything that doesn't need to be shown (i.e. backpack was enabled in editor to see how it looks)
        _backpack.SetActive(false);

        // Set things to correspond with storydata on Awake (first value is a dummy value)
        UpdateBackpackVisual(false, StoryDatastore.Instance.PickedUpBackpack.Value);
        // An important distinction -- AmberWornClothing differs from ChosenClothing
        UpdateClothesVisual(ClothingOption.Dirty, StoryDatastore.Instance.AmberWornClothing.Value);
        // Update hair
        UpdateHairVisual(HairOption.BONNET, StoryDatastore.Instance.AmberHairOption.Value);

        // Subscribe to changes in storydata for each visual
        StoryDatastore.Instance.PickedUpBackpack.Changed += UpdateBackpackVisual;
        StoryDatastore.Instance.AmberWornClothing.Changed += UpdateClothesVisual;
        StoryDatastore.Instance.AmberHairOption.Changed += UpdateHairVisual;
    }

    void UpdateBackpackVisual(bool oldValue, bool newValue) {
        _backpack.SetActive(newValue);
    }
    void UpdateClothesVisual(ClothingOption oldValue, ClothingOption newValue) { 
        // update texture to newValue
    }
    void UpdateHairVisual(HairOption oldValue, HairOption newValue) {
        // update hair to newValue
    }
}
