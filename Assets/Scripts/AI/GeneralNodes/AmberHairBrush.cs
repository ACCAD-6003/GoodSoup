using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class AmberHairBrush : Node
    {
        bool hairBrushed = false;
        public override NodeState Evaluate() {
            if (!hairBrushed) {
                hairBrushed = true;
                // Plug Amber Hair Changing logic into here
                StoryDatastore.Instance.AmberHairOption.Value = AmberVisual.HairOption.CLEAN;
            }
            return NodeState.SUCCESS;
        }
    }
}
