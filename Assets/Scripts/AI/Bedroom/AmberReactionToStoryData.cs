using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class IAmberReaction
{
    public abstract void PerformReaction();
}  

namespace Assets.Scripts.AI.Bedroom
{

    internal class AmberReactionToStoryData<T> : Node
    {
        private Dictionary<T, IAmberReaction> _reactions;
        private StoryData<T> _storyData;
        private bool _reacted = false;
        public AmberReactionToStoryData(Dictionary<T, IAmberReaction> reactions, StoryData<T> storyData) {
            _reactions = reactions;
            _storyData = storyData;
        }
        public override NodeState Evaluate()
        {
            if (!_reacted) {
                _reacted = true;
                _reactions[_storyData.Value].PerformReaction();
            }
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}
