using UnityEngine;
using static StoryDatastore;

public class SkipIfStoryDatastoreState : ISkipCondition
{
    public StoryDataType _storyData;
    public string _necessaryValueToSkip;
    public bool _skipIfDNE = false;
    public override bool ShouldSkip() {
        if (Instance == null) {
            Debug.LogError("Instance is null in skipifstorydatastorestate.cs");
        }
        dynamic storyDataValue = Instance.GetStoryDataValue(_storyData);
        dynamic necessaryDataValue = Instance.DeserializeStoryDataValue(_storyData, _necessaryValueToSkip);
        Debug.Log("Does " + storyDataValue + " Equal "  + necessaryDataValue + "??");

        return _skipIfDNE ? !storyDataValue.Equals(necessaryDataValue) : storyDataValue.Equals(necessaryDataValue);
            
    }
}