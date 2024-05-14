using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.AI.Kitchen
{
    public enum MoveDir { UP, DOWN, LEFT, RIGHT }
    public class MoveToTile : Node
    {
        public static Dictionary<MoveDir, Vector3> MoveDirections = new()
        {
            { MoveDir.UP, new Vector3(0,0,1) },
            { MoveDir.DOWN, new Vector3(0,0,-1) },
            { MoveDir.LEFT, new Vector3(-1,0,0) },
            { MoveDir.RIGHT, new Vector3(1, 0, 0) }
        };
        public grid_manager _grid;
        bool _targetSet = false, _destReached = false;
        public tile _tile;
        public MoveDir _dirAtEnd;
        private void ReachedDest() {
            _destReached = true;
            _grid.char_s.SetArbitraryRot(MoveDirections[_dirAtEnd]);
        }
        public override NodeState Evaluate()
        {
            if (!_targetSet) {
                _targetSet = true;
                _grid.Target(_tile, ReachedDest);
                _grid.char_s.SetLookRotWhenComplete(MoveDirections[_dirAtEnd]);
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
