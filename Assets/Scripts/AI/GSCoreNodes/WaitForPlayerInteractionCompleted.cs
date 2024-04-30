using BehaviorTree;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class WaitForPlayerInteractionCompleted : Node
    {
        public readonly InteractableObject _interaction;
        private bool _performed = false;
        public void Awake()
        {
            _interaction.PlayerInteraction.OnActionEnding += InteractionCompleted;
        }
        private void InteractionCompleted() {
            _performed = true;
        }
        public override NodeState Evaluate() {
            return _performed ?  NodeState.SUCCESS : NodeState.RUNNING;
        }
    }
}