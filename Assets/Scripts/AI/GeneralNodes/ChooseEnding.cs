using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Ending { FAR_CRY, PARANOID, GOOD_ENDING, BAD_ENDING, KICKED_OUT_OF_COLLEGE, GOOD_SOUP , ACADEMIC_WEAPON, BURNT_DOWN, MID_SOUP, LOCKED_OUT, TOUCH_GRASS, NULL }

namespace Assets.Scripts.AI.GeneralNodes
{
    public class SelectEnding : BehaviorTree.Node
    {
        private bool _chosen = false;
        Dictionary<StoryData<float>, Ending> endings = new Dictionary<StoryData<float>, Ending>() {
            { StoryDatastore.Instance.Annoyance, Ending.BAD_ENDING },
            { StoryDatastore.Instance.Paranoia, Ending.PARANOID },
            { StoryDatastore.Instance.Happiness, Ending.GOOD_ENDING },
        };
        public override NodeState Evaluate()
        {
            Debug.Log("FINAL ANNOYANCE: " + StoryDatastore.Instance.Annoyance.Value);
            Debug.Log("FINAL HAPPINESS: " + StoryDatastore.Instance.Happiness.Value);
            Debug.Log("FINAL PARANOIA: " + StoryDatastore.Instance.Paranoia.Value);

            if (!_chosen) {
                _chosen = true;
                float maxStat = -1f;
                Ending chosenEnding = Ending.FAR_CRY;
                foreach (var ending in endings) {
                    if (ending.Key.Value > maxStat) { 
                        maxStat = ending.Key.Value;
                        chosenEnding = ending.Value;
                    }
                }
                StoryDatastore.Instance.ChosenEnding.Value = chosenEnding;
                if (StoryDatastore.Instance.GoodSoupPuzzleSolved.Value && chosenEnding != Ending.BURNT_DOWN)
                {
                    if (StoryDatastore.Instance.FoodQuality.Value < 0f)
                    {
                        StoryDatastore.Instance.ChosenEnding.Value = Ending.MID_SOUP;
                    }
                    else {
                        StoryDatastore.Instance.ChosenEnding.Value = Ending.GOOD_SOUP;
                        SceneManager.LoadScene("GoodSoupCutscene");
                    }

                    state = NodeState.SUCCESS;
                    return NodeState.SUCCESS;
                }
                else if (StoryDatastore.Instance.EmailState.Value == ComputerHUD.EmailState.NICE_EMAIL_CONFIRMED && StoryDatastore.Instance.FoodQuality.Value > 5f)
                {
                    StoryDatastore.Instance.ChosenEnding.Value = Ending.ACADEMIC_WEAPON;
                }
                SceneManager.LoadScene("Endings");
            }
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}
