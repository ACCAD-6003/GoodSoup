using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(fileName = "UIIcons", menuName = "ScriptableObjects/UIIcons", order = 1)]
    public class UIElements : SerializedScriptableObject
    {
        public enum BubbleIcon { HAPPY, SAD, PARANOID, ANNOYANCE, SLEEPING, PHONE }
        public Dictionary<BubbleIcon, Sprite> IconImages;
    }
}
