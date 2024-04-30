using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class PerformAmberInteraction : IEvaluateOnce
    {
        private bool _performed = false;
        public InteractableObject interaction;
        public override void Run()
        {
            interaction.AmberInteraction.StartAction();
        }
    }
}
