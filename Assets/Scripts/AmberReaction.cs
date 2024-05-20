using Assets.Scripts.UI;
using UnityEngine;
using static StoryDatastore;

public class AmberReaction : MonoBehaviour
{
    public UIElements.BubbleIcon icon;
    public float impactOnStoryData;
    public StoryDataType type;
    public void PerformReaction()
    {
        UIManager.Instance.DisplaySimpleBubbleTilInterrupted(icon);
        var value = StoryDatastore.Instance.GetStoryDataValue(type);
        value += impactOnStoryData;
        StoryDatastore.Instance.SetStoryDataValue(type, value);
        //StoryDatastore.Instance.Happiness.Value += 2f;
    }
}