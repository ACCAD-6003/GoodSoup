using BehaviorTree;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class WaitFor : Node
    {
        public float _seconds;
        private float _startTime;

        public WaitFor(float seconds)
        {
            _seconds = seconds;
        }

        public override NodeState Evaluate()
        {
            if (_startTime == 0f)
            {
                _startTime = Time.time;
                state = NodeState.RUNNING;
                return NodeState.RUNNING;
            }
            else if (Time.time - _startTime >= _seconds)
            {
                state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
            else
            {
                state = NodeState.RUNNING;
                return NodeState.RUNNING;
            }
        }
    }
}