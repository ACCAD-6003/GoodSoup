using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.Kitchen
{
    public class CheckIfICareAboutBurner : Node
    {
        private StoryDatastore _dataStore;
        public CheckIfICareAboutBurner(StoryDatastore data) { 
            _dataStore = data;
        }
        public override NodeState Evaluate()
        {
            if (_dataStore.BurnerHeat.Value >= 150f) {
                state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
            state = NodeState.RUNNING;
            return NodeState.RUNNING;
        }
    }
}
