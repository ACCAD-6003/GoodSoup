using Assets.Scripts.UI;
using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class EvaluateItemsPickedUp : IEvaluateOnce
    {
        public List<int> _identifiers;
        public UIElements.BubbleIcon _happyIcon, _sadIcon;
        void AllItemsNotPickedUp() {
            UIManager.Instance.DisplaySimpleBubbleForSeconds(_sadIcon, 2f);
            StoryDatastore.Instance.ResultOfEvaluation.Value = false;
        }
        void AllItemsPickedUp()
        {
            UIManager.Instance.DisplaySimpleBubbleForSeconds(_happyIcon, 2f);
            StoryDatastore.Instance.ResultOfEvaluation.Value = true;
        }
        public override void Run() {
            foreach (var identifier in _identifiers) {
                if (!StoryDatastore.Instance.MoveObjects.ContainsKey(identifier)) {
                    AllItemsNotPickedUp();
                    return;
                }
                else if (!StoryDatastore.Instance.MoveObjects[identifier].Value)
                {
                    AllItemsNotPickedUp();
                    return;
                }
            }
            AllItemsPickedUp();
        }
    }
}
