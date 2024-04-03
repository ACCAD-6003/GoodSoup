using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class DebugNode : Node
    {
        private int index;
        private bool evaluated = false;
        public DebugNode(int index) {
            this.index = index;
        }
        public override NodeState Evaluate()
        {
            if (!evaluated)
            {
                evaluated = true;
                Debug.Log(index);
            }
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}
