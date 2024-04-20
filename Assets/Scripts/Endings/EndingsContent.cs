using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndingsContent", menuName = "ScriptableObjects/EndingsContent", order = 1)]
public class EndingsContent : SerializedScriptableObject
{
    public Dictionary<Ending, EndingContent> EndingContent;
}
public class EndingContent {
    public string DisplayName, Description, DoBetterMessage, FiveStarMessage;
    public Sprite imageSprite;
}