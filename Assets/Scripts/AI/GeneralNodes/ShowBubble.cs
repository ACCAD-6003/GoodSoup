using Assets.Scripts.UI;
using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class ShowBubble : Node
    {
        private bool _isTimed = false;
        private UIElements.BubbleIcon _icon;
        private float _time;
        private bool _performed = false;

        public ShowBubble(UIElements.BubbleIcon icon) {
            _icon = icon;
        }
        public ShowBubble(UIElements.BubbleIcon icon, float time)
        {
            _icon = icon;
            _isTimed = true;
            _time = time;
        }
        public override NodeState Evaluate()
        {
            if (!_performed)
            {
                _performed = true;
                if (_isTimed)
                {
                    UIManager.Instance.DisplaySimpleBubbleForSeconds(_icon, _time);
                }
                else {
                    UIManager.Instance.DisplaySimpleBubbleTilInterrupted(_icon);
                }
            }
            return NodeState.SUCCESS;
        }
    }
}
