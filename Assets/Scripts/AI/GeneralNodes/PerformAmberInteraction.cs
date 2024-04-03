using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class PerformAmberInteraction : Node
    {
        private bool _performed = false;
        private Interaction interaction;
        public PerformAmberInteraction(Interaction interaction) { 
            this.interaction = interaction;
        }
        public override NodeState Evaluate()
        {
            if (!_performed) { 
                _performed = true;
                interaction.StartAction();
            }
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}
