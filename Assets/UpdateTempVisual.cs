using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTempVisual : MonoBehaviour
{
    public OvenTempSprites sprites;
    public Image tempImage;
    private Dictionary<(float min, float max), HeatSetting> tempRanges;
    private HeatSetting currHeat;
    private void Awake()
    {
        // make sure u unsubscribe ur events aaron
        tempRanges = new Dictionary<(float min, float max), HeatSetting>()
        {
            {(float.MinValue, 85), HeatSetting.LOW_TEMP},
            {(85, 99), HeatSetting.MEDIUM_TEMP},
            {(99, float.MaxValue), HeatSetting.HIGH_TEMP}
        };
        currHeat = GetHeatSettingFromTemp(StoryDatastore.Instance.ShowerTemperature.Value);
        RefreshImage();
        StoryDatastore.Instance.ShowerTemperature.Changed += ShowerTemperature_Changed;
    }
    private void OnDestroy()
    {
        StoryDatastore.Instance.ShowerTemperature.Changed -= ShowerTemperature_Changed;
    }
    private void RefreshImage() {
        tempImage.sprite = sprites.sprites[currHeat];
    }

    private void ShowerTemperature_Changed(float oldVal, float newVal)
    {
        var newHeat = GetHeatSettingFromTemp(StoryDatastore.Instance.ShowerTemperature.Value);
        if (currHeat != newHeat) {
            currHeat = newHeat;
            RefreshImage();
        }
    }

    private HeatSetting GetHeatSettingFromTemp(float temp) {
        foreach (var range in tempRanges)
        {
            if (temp >= range.Key.min && temp < range.Key.max)
            {
                return range.Value;
            }
        }
        throw new ArgumentOutOfRangeException(nameof(temp), "Value is outside the defined range.");
    }
}
