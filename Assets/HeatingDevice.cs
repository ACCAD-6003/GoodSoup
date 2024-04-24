using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingDevice : MonoBehaviour
{
    bool cooking = false;
    HeatSetting setting = HeatSetting.LOW_TEMP;
    Dictionary<HeatSetting, float> timeUnderHeat = new() {
        { HeatSetting.LOW_TEMP, 0f },
        { HeatSetting.MEDIUM_TEMP, 0f },
        { HeatSetting.HIGH_TEMP, 0f }
    };
    void Update()
    {
        if (!cooking && StoryDatastore.Instance.ActivelyCooking.Value && !StoryDatastore.Instance.GoodSoupPuzzleSolved.Value)
        {
            // start cooking
            cooking = true;
        }
        else if (cooking && !StoryDatastore.Instance.ActivelyCooking.Value)
        {
            // stop cooking
            cooking = false;
        }
        else {
            if (StoryDatastore.Instance.HeatSetting.Value != setting) {
                if (setting == HeatSetting.HIGH_TEMP) { 
                    // kill particles
                }
                setting = StoryDatastore.Instance.HeatSetting.Value;
            }
            timeUnderHeat[setting] += Time.deltaTime;
        }
        
    }
}
