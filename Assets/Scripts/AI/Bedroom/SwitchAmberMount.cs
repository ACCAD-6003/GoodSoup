using BehaviorTree;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class SwitchAmberMount : Node
    {
        public AmberMount _mount;
        private bool _mounted = false, _mounting = false;
        public override NodeState Evaluate()
        {
            if (!_mounted && !_mounting) {
                _mounting = true;
                _mount.Mount();
                
                _mount.CompletedMounting += FinishMounting;
            }
            if (_mounted)
            {
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING;
        }
        private void FinishMounting() {
            _mounted = true;
        }
    }
}