using BehaviorTree;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using static StoryDatastore;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class ModifyStat : IEvaluateOnce
    {
        [Header("This will add a value (positive or negative) to a story data value. It must be a float.")]
        public StoryDataType _data;
        public float _value;
        public override void Run() {
            var prevStatValue = Instance.GetStoryDataValue(_data);
            Instance.SetStoryDataValue(_data, prevStatValue + _value + "");
        }
    }
}
