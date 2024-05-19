using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Scripts.AI.Bedroom
{
    public class StopFarCryEnding : IEvaluateOnce
    {
        public FarCrySwitcher switcher;
        public override void Run()
        {
            if (switcher != null) {
                switcher.Stop();
            }
        }
    }
}
