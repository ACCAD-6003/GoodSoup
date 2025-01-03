﻿using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Ending { FAR_CRY, PARANOID, GOOD_ENDING, BAD_ENDING, KICKED_OUT_OF_COLLEGE, GOOD_SOUP , ACADEMIC_WEAPON, BURNT_DOWN, MID_SOUP, LOCKED_OUT, TOUCH_GRASS }

namespace Assets.Scripts.AI.GeneralNodes
{
    public class SelectGenericEnding : IEvaluateOnce
    {
        Dictionary<StoryData<float>, Ending> endings = new Dictionary<StoryData<float>, Ending>() {
            { StoryDatastore.Instance.Annoyance, Ending.BAD_ENDING },
            { StoryDatastore.Instance.Paranoia, Ending.PARANOID },
            { StoryDatastore.Instance.Happiness, Ending.GOOD_ENDING },
        };
        public override void Run()
        {
            Debug.Log("FINAL ANNOYANCE: " + StoryDatastore.Instance.Annoyance.Value);
            Debug.Log("FINAL HAPPINESS: " + StoryDatastore.Instance.Happiness.Value);
            Debug.Log("FINAL PARANOIA: " + StoryDatastore.Instance.Paranoia.Value);

            float maxStat = -1f;
            Ending chosenEnding = Ending.FAR_CRY;

            foreach (var ending in endings) {
                if (ending.Key.Value > maxStat) { 
                    maxStat = ending.Key.Value;
                    chosenEnding = ending.Value;
                }
            }

            StoryDatastore.Instance.ChosenEnding.Value = chosenEnding;

            Debug.Log(StoryDatastore.Instance.EmailState.Value + " EMAIL STATE");
            Debug.Log(StoryDatastore.Instance.FoodQuality.Value + " FOOD QUALITY");
            if (StoryDatastore.Instance.EmailState.Value == ComputerHUD.EmailState.NICE_EMAIL_CONFIRMED && StoryDatastore.Instance.FoodQuality.Value > 2.5f)
            {
                StoryDatastore.Instance.ChosenEnding.Value = Ending.ACADEMIC_WEAPON;
            }
            SceneManager.LoadScene("Endings");
        }
    }
}
