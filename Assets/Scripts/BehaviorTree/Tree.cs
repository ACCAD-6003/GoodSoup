using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] Node _root = null;
        private void Update()
        {
            _root?.Evaluate();
        }
    }
}
