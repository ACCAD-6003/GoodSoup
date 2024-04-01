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
        public enum EmailState { NOTHING_CHANGED = 0, MEAN_EMAIL = 1, NICE_EMAIL = 2, MEAN_EMAIL_CONFIRMED = 3, NICE_EMAIL_CONFIRMED = 4, EMAIL_SENT = 5 }
        public Dictionary<EmailState, GameObject> EmailScreens;
    }
}
