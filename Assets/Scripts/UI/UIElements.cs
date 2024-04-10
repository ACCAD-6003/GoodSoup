using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(fileName = "UIIcons", menuName = "ScriptableObjects/UIIcons", order = 1)]
    public class UIElements : SerializedScriptableObject
    {
        public enum BubbleIcon { HAPPY, HAPPY_SUNSHINE, SAD, PARANOID, ANNOYANCE, SLEEPING, PHONE, COLD, BRUSH, SHOWERING, TOOTHBRUSH, OH_WHERE_IS_MY_HAIRBRUSH, HAPPY_LAUNDRY, SAD_LAUNDRY,
        DIRTY_OUTFIT, SICK_OUTFIT }
        public Dictionary<BubbleIcon, Sprite> IconImages;
    }
}
