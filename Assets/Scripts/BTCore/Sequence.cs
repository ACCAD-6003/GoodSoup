using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public List<Node> children;
        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return NodeState.RUNNING;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = NodeState.SUCCESS;
            return state;
        }
    }
}
