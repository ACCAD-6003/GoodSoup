using BehaviorTree;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class WaitFor : Node
    {
        private readonly float _seconds;
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
                return NodeState.RUNNING;
            }
            else if (Time.time - _startTime >= _seconds)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.RUNNING;
            }
        }
    }
}