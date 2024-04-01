using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(fileName = "UIIcons", menuName = "ScriptableObjects/UIIcons", order = 1)]
    internal class UIIcons : SerializedScriptableObject
    {
        public enum BubbleIcon { HAPPY, SAD, PARANOID, ANNOYANCE, SLEEPING, PHONE }
        public Dictionary<BubbleIcon, Sprite> IconImages;
    }
}
