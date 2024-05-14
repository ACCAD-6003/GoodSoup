using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Shirts : SerializedMonoBehaviour
{
    public bool BedShirt = false;
    public Dictionary<ClothingOption, GameObject> AllShirts = new Dictionary<ClothingOption, GameObject>();
    private void Awake()
    {
        if (BedShirt) {
            foreach (var shirt in AllShirts)
            {
                shirt.Value.SetActive(false);
            }
            if (StoryDatastore.Instance.ChosenClothing.Value == ClothingOption.NONE) {
                return;
            }
            AllShirts[StoryDatastore.Instance.ChosenClothing.Value].SetActive(true);
        }
    }
}