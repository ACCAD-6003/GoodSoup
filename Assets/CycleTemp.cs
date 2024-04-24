using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleTemp : Interaction
{
    [SerializeField] OvenTempSprites sprites;
    [SerializeField] Image tempImage;
    public override void DoAction()
    {
        Debug.Log("Old Value: " + StoryDatastore.Instance.HeatSetting.Value + " New Value: " + (HeatSetting)((((int)StoryDatastore.Instance.HeatSetting.Value) + 1) % 3));
        StoryDatastore.Instance.HeatSetting.Value = (HeatSetting) ((((int) StoryDatastore.Instance.HeatSetting.Value) + 1) % 3);
        tempImage.sprite = sprites.sprites[StoryDatastore.Instance.HeatSetting.Value];
        EndAction();
    }
    public override void LoadData(StoryDatastore data)
    {

    }
    public override void SaveData(StoryDatastore data)
    {

    }
}