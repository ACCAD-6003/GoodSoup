using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class SkipIfStoryDatastoreState<T> : ISkipCondition {
        StoryData<T> _storyData;
        T _necessaryValueToSkip;
        public SkipIfStoryDatastoreState(StoryData<T> storyData, T necessaryValueToSkip) {
            _storyData = storyData;
            _necessaryValueToSkip = necessaryValueToSkip;
        }
        public bool ShouldSkip() {
            return _storyData.Value.Equals(_necessaryValueToSkip);
        }
    }
    public interface ISkipCondition
    {
        public bool ShouldSkip();
    }
    public class WrapperNode : Node
    {
        private ISkipCondition _skipCondition;
        private Sequence _sequence;
        public WrapperNode(ISkipCondition skipCondition, List<Node> nodes) : base(nodes) {
            _skipCondition = skipCondition;
            _sequence = new Sequence(nodes);
        }
        public override NodeState Evaluate()
        {
            if (_skipCondition.ShouldSkip()) {
                return NodeState.SUCCESS;
            }
            return _sequence.Evaluate();
        }
    }
}
