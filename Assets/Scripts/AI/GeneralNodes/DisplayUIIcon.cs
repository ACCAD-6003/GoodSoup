using Assets.Scripts.UI;
using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.UI.UIElements;

namespace Assets.Scripts.AI
{
    internal class DisplayUIIcon : Node
    {
        private readonly BubbleIcon _icon;
        private bool _performed = false;
        public DisplayUIIcon(BubbleIcon icon)
        {
            _icon = icon;
        }
        public override NodeState Evaluate()
        {
            if (!_performed) { 
                _performed = true;
                UIManager.Instance.DisplaySimpleBubbleTilInterrupted(_icon);
            }
            state = _performed ? NodeState.SUCCESS : NodeState.RUNNING;
            return _performed ? NodeState.SUCCESS : NodeState.RUNNING;
        }
    }
}