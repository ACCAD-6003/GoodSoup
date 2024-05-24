public class GoodSouped : ISkipCondition
{
    public override bool ShouldSkip()
    {
        return StoryDatastore.Instance.GoodSoupPuzzleSolved.Value;
    }
}