﻿using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public enum Ending { FAR_CRY, PARANOID, GOOD_ENDING, BAD_ENDING }
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
                SceneManager.LoadScene("Endings");
            }
            return NodeState.SUCCESS;
        }
    }
}