using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class ModifyStat : Node
    {
        private StoryData<float> _data;
        private float _value;
        private bool _modified = false;
        public ModifyStat(StoryData<float> data, float value) {
            _data = data;
            _value = value;
        }
        public override NodeState Evaluate() {
            if (!_modified) {
                _modified = true;
                _data.Value = _value;
            }
            return NodeState.SUCCESS;
        }
    }
}
