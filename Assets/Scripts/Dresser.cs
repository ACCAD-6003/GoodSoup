using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// none is used for when there is no shirt on the hanger (after amber picks it up)
public enum ClothingOption { 
    BLUE, GREEN, ORANGE, DIRTY, PAJAMAS, TOWEL, NONE
}
public class Dresser : Interaction
{
    [SerializeField] Shirts shirtsOnBed;

    public static Dictionary<ClothingOption, string> ClothingOptions = new()
    {
        { ClothingOption.DIRTY, "AmberDirtyClothes" },
        { ClothingOption.BLUE, "AmberDayClothes" },
        { ClothingOption.GREEN, "AmberDayClothes2" },
        { ClothingOption.ORANGE, "AmberDayClothes3" },
        // Untested idk if this is the name of the pajamas or not
        { ClothingOption.PAJAMAS, "AmberPajamas" },
        { ClothingOption.TOWEL, "AmberNaked" },
        { ClothingOption.NONE, "AmberNaked" },
    };

    void Awake()
    {

    }

    public override void LoadData(StoryDatastore data)
    {

    }

    public override void SaveData(StoryDatastore data)
    {

    }

    public override void DoAction()
    {
        //Texture2D clothesTexture = Resources.Load<Texture2D>("Textures/" + ClothingOptions[StoryDatastore.Instance.ChosenClothing.Value]);
        //clothes.SetTexture("_MainTex", clothesTexture);
        StoryDatastore.Instance.AmberWornClothing.Value = StoryDatastore.Instance.ChosenClothing.Value;
        StoryDatastore.Instance.AmberDressed.Value = true;
        foreach (var shirt in shirtsOnBed.AllShirts) {
            shirt.Value.SetActive(false);
        }
        StoryDatastore.Instance.ChosenClothing.Value = ClothingOption.NONE;
        EndAction();
    }
}
