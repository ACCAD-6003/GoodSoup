using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFrames : SerializedScriptableObject
{
    public Dictionary<int, (Sprite sprite, AudioClip sound)> frames;
}