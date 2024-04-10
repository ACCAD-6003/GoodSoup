using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ClothingOption { 
    Blue, Green, Orange, Dirty
}
public class Dresser : Interaction
{
    public static Dictionary<ClothingOption, string> ClothingOptions = new()
    {
        { ClothingOption.Dirty, "AmberDirtyClothes" },
        { ClothingOption.Blue, "AmberDayClothes" },
        { ClothingOption.Green, "AmberDayClothes2" },
        { ClothingOption.Orange, "AmberDayClothes3"}
    };

    Material clothes;

    void Awake()
    {
        clothes = GameObject.FindGameObjectWithTag("AmberTorso").GetComponent<MeshRenderer>().material;
    }

    public override void LoadData(StoryDatastore data)
    {

    }

    public override void SaveData(StoryDatastore data)
    {

    }

    protected override void DoAction()
    {
        Texture clothesTexture = Resources.Load<Texture>("Textures/" + StoryDatastore.Instance.ChosenClothing);
        clothes.SetTexture("_MainTex", clothesTexture);
        //StoryDatastore.Instance.AmberDressed.Value = true;
        EndAction();
    }
}
