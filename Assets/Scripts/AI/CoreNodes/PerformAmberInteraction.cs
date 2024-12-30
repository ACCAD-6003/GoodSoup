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
        public InteractableObject interaction;
        public override void Run()
        {
            interaction.AmberInteraction.StartAction();
        }
    }
}
