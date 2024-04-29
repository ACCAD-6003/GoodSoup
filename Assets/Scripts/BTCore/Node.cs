using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    { 
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node : SerializedMonoBehaviour
    {
        protected NodeState state;
        [NonSerialized] public Node parent;
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        public Node() {
            parent = null;
        }
        public Node(List<Node> children) { 
            foreach(Node child in children) { 
                Attach(child);
            }
        }
        private void Attach(Node node) { 
            node.parent = this;
        }
        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}
