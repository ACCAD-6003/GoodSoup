using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI.Kitchen
{
    public class MoveToTile : Node
    {
        public grid_manager _grid;
        bool _targetSet = false, _destReached = false;
        public tile _tile;
        public Vector3 _dirAtEnd;
        public bool rotateAtTheEnd = false;
        private void ReachedDest() {
            _destReached = true;
            if (rotateAtTheEnd) {
                _grid.char_s.SetArbitraryRot(_dirAtEnd);
            }
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
