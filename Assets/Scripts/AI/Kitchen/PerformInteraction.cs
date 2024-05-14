using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    public class PerformInteraction : Node
    {
        private InteractableObject _interactableObject;
        private bool _interactionStarted = false, _interactionCompleted = false;
        private grid_manager _grid;
        public PerformInteraction(InteractableObject intObj, grid_manager grid_Manager) {
            _interactableObject = intObj;
            _grid = grid_Manager;
        }
        private void InteractionCompleted() {
            _interactionCompleted = true;
        }
        public override NodeState Evaluate()
        {
            if (!_interactionStarted) {
                _grid.SetArbitraryLookRot(_interactableObject.AmberInteraction.AssociatedDirection);
                _interactionStarted = true;
                _interactableObject.AmberInteraction.StartAction();
                _interactableObject.AmberInteraction.OnActionEnding += InteractionCompleted;
            }
            if (_interactionCompleted) {
                state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
            state = NodeState.RUNNING;
            return NodeState.RUNNING;
        }
    }
}
