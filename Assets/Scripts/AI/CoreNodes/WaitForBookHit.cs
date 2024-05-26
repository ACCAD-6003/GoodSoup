using BehaviorTree;
using System;

namespace Assets.Scripts.AI
{
    internal class WaitForBookHit : Node
    {
        private bool _performed = false;
        public void Awake()
        {
            StoryDatastore.Instance.AnyBookDropped.Changed += InteractionCompleted;
        }
        private void InteractionCompleted(bool b, bool b2)
        {
            if (!b2) {
                return;
            }
            if (!StoryDatastore.Instance.AmberOutOfBed.Value)
            {
                StoryDatastore.Instance.Annoyance.Value += 1f;
            }
            _performed = true;
        }
        public override NodeState Evaluate()
        {
            return _performed ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}