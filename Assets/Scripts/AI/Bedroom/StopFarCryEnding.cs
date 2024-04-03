using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Scripts.AI.Bedroom
{
    public class StopFarCryEnding : Node
    {
        bool done = false;
        FarCrySwitcher switcher;
        public StopFarCryEnding(FarCrySwitcher switcher) {
            this.switcher = switcher;
        }
        public override NodeState Evaluate()
        {
            if (!done)
            {
                done = true;
                switcher.Stop();
            }
            return NodeState.SUCCESS;
        }
    }
}
