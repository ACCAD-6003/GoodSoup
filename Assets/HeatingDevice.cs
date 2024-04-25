using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public CycleTemp ct;
    public AudioSource src;
    public AudioClip fireAlarm;
    public bool isOven = false;
    public GameObject SmokeFillRoom;
    bool notAlreadyTransitioningAway = true;
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
            ct.EndAction();
        }
        else if (cooking && !StoryDatastore.Instance.ActivelyCooking.Value)
        {
            // stop cooking
            cooking = false;
            TempCanvas.SetActive(false);
            particles.Stop();
            ct.PutInProgress();
            HeatSetting settingWithMostTimeSpentOnIt = HeatSetting.LOW_TEMP;
            float mostTimeSpent = -1f;
            foreach (var heatSetting in timeUnderHeat) {
                if (heatSetting.Value > mostTimeSpent) {
                    settingWithMostTimeSpentOnIt = heatSetting.Key;
                    mostTimeSpent = heatSetting.Value;
                } 
            }

            switch (settingWithMostTimeSpentOnIt) {
                case HeatSetting.HIGH_TEMP:
                    StoryDatastore.Instance.FoodQuality.Value -= mostTimeSpent;
                    break;
                case HeatSetting.MEDIUM_TEMP:
                    StoryDatastore.Instance.FoodQuality.Value -= mostTimeSpent;
                    break;
                case HeatSetting.LOW_TEMP:
                    StoryDatastore.Instance.FoodQuality.Value += 5f;
                    break;
            }
        }
        else {
            if (StoryDatastore.Instance.HeatSetting.Value != setting) {
                particles.emissionRate = TempParticleDictionary.particleCountPerTemp[StoryDatastore.Instance.HeatSetting.Value];
                if (setting == HeatSetting.HIGH_TEMP) { 
                    // kill particles
                }
                setting = StoryDatastore.Instance.HeatSetting.Value;
            }
            timeUnderHeat[setting] += Time.deltaTime;
            if (timeUnderHeat[HeatSetting.HIGH_TEMP] > 10f && isOven && notAlreadyTransitioningAway) {
                SmokeFillRoom.SetActive(true);
                src.PlayOneShot(fireAlarm);
                notAlreadyTransitioningAway = false;
                UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.OK_IM_COMING, 4f);
                StartCoroutine(TransitionAway());
            }
        }
        
    }
    IEnumerator TransitionAway() {
        StoryDatastore.Instance.ChosenEnding.Value = Ending.BURNT_DOWN;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Endings");
    }
}
