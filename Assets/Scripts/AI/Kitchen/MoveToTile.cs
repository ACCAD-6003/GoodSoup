using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        private Vector3 _dirAtEnd;
        public MoveToTile(grid_manager grid, tile tile) {
            _grid = grid;
            _tile = tile;
        }
        public MoveToTile(grid_manager grid, tile tile, Vector3 dirAtEnd) {
            _grid = grid;
            _tile = tile;
            _grid.SetLookRot(_dirAtEnd);
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
                state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
            state = NodeState.RUNNING;
            return NodeState.RUNNING;
        }
    }
}
