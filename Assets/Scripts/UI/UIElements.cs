using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(fileName = "UIIcons", menuName = "ScriptableObjects/UIIcons", order = 1)]
    public class UIElements : SerializedScriptableObject
    {
        public enum BubbleIcon { HAPPY, SAD, PARANOID, ANNOYANCE, SLEEPING, PHONE, COLD, BRUSH, SHOWERING, TOOTHBRUSH, OH_WHERE_IS_MY_HAIRBRUSH }
        public Dictionary<BubbleIcon, Sprite> IconImages;
    }
}
