using Assets.Scripts.AI;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PutTowelOn : Interaction
{
    public override void DoAction()
    {
        StoryDatastore.Instance.AmberWornClothing.Value = ClothingOption.Towel;
        if (StoryDatastore.Instance.TowelHot.Value) {
            UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.HAPPY_TOWEL, 2f);
            StoryDatastore.Instance.Happiness.Value += 2f;
        }
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
