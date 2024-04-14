using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PutTowelOn : Interaction
{
    public override void DoAction()
    {
        StoryDatastore.Instance.AmberWornClothing.Value = ClothingOption.Towel;
        Destroy(gameObject);
    }

    public override void LoadData(StoryDatastore data)
    {
        // doesnt save yet
    }

    public override void SaveData(StoryDatastore data)
    {
        // doesnt save yet
    }
}
