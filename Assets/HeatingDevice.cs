using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingDevice : MonoBehaviour
{
    public GameObject TempCanvas;
    bool cooking = false;
    public ParticleSystem particles;
    HeatSetting setting = HeatSetting.LOW_TEMP;
    Dictionary<HeatSetting, float> timeUnderHeat = new() {
        { HeatSetting.LOW_TEMP, 0f },
        { HeatSetting.MEDIUM_TEMP, 0f },
        { HeatSetting.HIGH_TEMP, 0f }
    };
    public TempParticleDictionary TempParticleDictionary;
    private void Start()
    {
        particles.Stop();
    }

    [System.Obsolete]
    void Update()
    {
        if (!cooking && StoryDatastore.Instance.ActivelyCooking.Value && !StoryDatastore.Instance.GoodSoupPuzzleSolved.Value)
        {
            // start cooking
            cooking = true;
            TempCanvas.SetActive(true);
            particles.emissionRate = TempParticleDictionary.particleCountPerTemp[setting];
            particles.Play();
        }
        else if (cooking && !StoryDatastore.Instance.ActivelyCooking.Value)
        {
            // stop cooking
            cooking = false;
            TempCanvas.SetActive(false);
            particles.Stop();
        }
        else {
            if (StoryDatastore.Instance.HeatSetting.Value != setting) {
                particles.emissionRate = TempParticleDictionary.particleCountPerTemp[setting];
                if (setting == HeatSetting.HIGH_TEMP) { 
                    // kill particles
                }
                setting = StoryDatastore.Instance.HeatSetting.Value;
            }
            timeUnderHeat[setting] += Time.deltaTime;
        }
        
    }
}
