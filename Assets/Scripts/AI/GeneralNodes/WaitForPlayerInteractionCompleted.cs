using BehaviorTree;

namespace Assets.Scripts.AI
{
    internal class WaitForPlayerInteractionCompleted : Node
    {
        private readonly InteractableObject _interaction;
        private bool _performed = false;
        public WaitForPlayerInteractionCompleted(InteractableObject interaction)
        {
            _interaction = interaction;
            _interaction.PlayerInteraction.OnActionEnding += InteractionCompleted;
        }
        private void InteractionCompleted() {
            _performed = true;
        }
        public override NodeState Evaluate() {
            return _performed ? NodeState.RUNNING : NodeState.SUCCESS;
        }
    }
}