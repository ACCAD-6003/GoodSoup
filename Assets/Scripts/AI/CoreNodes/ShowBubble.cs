using Assets.Scripts.UI;
using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI.GeneralNodes
{
    public class ShowBubble : IEvaluateOnce
    {
        public bool _isTimed = false;
        public UIElements.BubbleIcon _icon;
        public float _time;
        public override void Run()
        {
            if (_isTimed)
            {
                UIManager.Instance.DisplaySimpleBubbleForSeconds(_icon, _time);
            }
            else {
                UIManager.Instance.DisplaySimpleBubbleTilInterrupted(_icon);
            }
        }
    }
}
