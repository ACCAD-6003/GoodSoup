using BehaviorTree;
using UnityEngine;

public class ResetKitchen : IEvaluateOnce
{
    public InteractableObject MoveTrayToBurner, MoveTrayToDinnerTable, MoveTrayToTable;
    public override void Run()
    {
        StoryDatastore.Instance.ActivelyCooking.Value = false;
        StoryDatastore.Instance.FoodQuality.Value = 0f;
        StoryDatastore.Instance.HeatSetting.Value = HeatSetting.LOW_TEMP;
        MoveTrayToBurner.gameObject.SetActive(false);
        MoveTrayToDinnerTable.gameObject.SetActive(false);
        MoveTrayToTable.gameObject.SetActive(false);
    }
}