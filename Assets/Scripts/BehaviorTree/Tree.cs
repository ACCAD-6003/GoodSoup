using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;
        protected void Start()
        {
            _root = SetupTree();
        }
        private void Update()
        {
            _root?.Evaluate();
        }
        protected abstract Node SetupTree();
    }
}
