using BehaviorTree;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class SwitchAmberMount : Node
    {
        private readonly AmberMount _mount;
        private bool _mounted = false, _mounting = false;
        public SwitchAmberMount(AmberMount sitInBed)
        {
            this._mount = sitInBed;
        }
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