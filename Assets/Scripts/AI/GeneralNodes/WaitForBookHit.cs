using BehaviorTree;
using System;

namespace Assets.Scripts.AI
{
    internal class WaitForBookHit : Node
    {
        private readonly InteractableObject _interaction;
        private bool _performed = false;
        public WaitForBookHit(StoryData<bool> s)
        {
            s.Changed += InteractionCompleted;
        }
        private void InteractionCompleted(bool b, bool b2)
        {
            if (!b2) {
                return;
            }
            StoryDatastore.Instance.Annoyance.Value += 1f;
            _performed = true;
        }
        public override NodeState Evaluate()
        {
            return _performed ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}