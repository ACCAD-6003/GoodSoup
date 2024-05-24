public class MovePerformed : ISkipCondition
{
    public int ID;
    public override bool ShouldSkip()
    {
        return StoryDatastore.Instance.MoveObjects[ID].Value;
    }
}