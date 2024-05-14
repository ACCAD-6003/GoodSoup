using BehaviorTree;
using static StoryDatastore;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class ModifyStat : IEvaluateOnce
    {
        public StoryDataType _data;
        public float _value;
        public override void Run() {
            var prevStatValue = ((StoryData<float>) Instance.GetStoryDataValue(_data)).Value;
            Instance.SetStoryDataValue(_data, prevStatValue + _value + "");
        }
    }
}
