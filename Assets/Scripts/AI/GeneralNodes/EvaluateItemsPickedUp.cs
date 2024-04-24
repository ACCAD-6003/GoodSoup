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
    public class EvaluateItemsPickedUp : Node
    {
        private bool _evaluated = false;
        private List<int> _identifiers;
        private UIElements.BubbleIcon _happyIcon, _sadIcon;
        public EvaluateItemsPickedUp(List<int> identifiers, UIElements.BubbleIcon happyIcon, UIElements.BubbleIcon sadIcon) { 
            _identifiers = identifiers;
            _happyIcon = happyIcon;
            _sadIcon = sadIcon;
        }
        void AllItemsNotPickedUp() {
            UIManager.Instance.DisplaySimpleBubbleForSeconds(_sadIcon, 2f);
            StoryDatastore.Instance.ResultOfEvaluation.Value = false;
        }
        void AllItemsPickedUp()
        {
            UIManager.Instance.DisplaySimpleBubbleForSeconds(_happyIcon, 2f);
            StoryDatastore.Instance.ResultOfEvaluation.Value = true;
        }
        public override NodeState Evaluate() {
            if (!_evaluated) {
                _evaluated = true;
                foreach (var identifier in _identifiers) {
                    if (!StoryDatastore.Instance.MoveObjects.ContainsKey(identifier)) {
                        AllItemsNotPickedUp();
                        return NodeState.SUCCESS;
                    }
                    if (!StoryDatastore.Instance.MoveObjects[identifier].Value)
                    {
                        AllItemsNotPickedUp();
                        return NodeState.SUCCESS;
                    }
                }
                AllItemsPickedUp();
            }
            return NodeState.SUCCESS;
        }
    }
}
