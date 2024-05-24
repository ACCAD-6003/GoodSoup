using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveFridgeMagnets : IEvaluateOnce
{
    public static bool noticedLetters = false;
    public override void Run()
    {
        if (StoryDatastore.Instance.MoveObjects[77].Value && !noticedLetters)
        {
            noticedLetters = true;
            UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.PARANOID, 3f);
            StoryDatastore.Instance.Paranoia.Value += 3f;
        }
    }
}
