using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.Kitchen
{
    /// <summary>
    /// This Task guides Amber to an interactable object.
    /// </summary>
    public class MoveToTile : Node
    {
        private grid_manager _grid;
        bool _targetSet = false, _destReached = false;
        private tile _tile;
        public MoveToTile(grid_manager grid, tile tile) {
            _grid = grid;
            _tile = tile;
        }
        private void ReachedDest() {
            _destReached = true;
        }
        public override NodeState Evaluate()
        {
            if (!_targetSet) {
                _targetSet = true;
                _grid.Target(_tile, ReachedDest);
            }
            if (_destReached) {
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING;
        }
    }
}
