using BehaviorTree;
using static StoryDatastore;

public class ChangeStoryData : IEvaluateOnce
{
    public string newValue;
    public StoryDataType _storyData;
    public override void Run()
    {
        Instance.SetStoryDataValue(_storyData, newValue);
    }
}