using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class DebugNode : IEvaluateOnce
    {
        public int index;
        public override void Run()
        {
            Debug.Log(index);
        }
    }
}
