using Assets.Scripts.UI;
using BehaviorTree;

public class EvaluateFood : IEvaluateOnce
{
    public override void Run()
    {
        if (StoryDatastore.Instance.FoodQuality.Value <= 0f)
        {
            UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.DAMN_THIS_FOOD_BLOWS, 3f);
            StoryDatastore.Instance.Annoyance.Value += 3f;
        }
        else if (StoryDatastore.Instance.FoodQuality.Value > 0f)
        {
            UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.POG_CHEF, 3f);
            StoryDatastore.Instance.Happiness.Value += 3f;
        }
    }
}