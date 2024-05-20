using System.Collections.Generic;

namespace Assets.Scripts.AI.Bedroom
{

    public class AmberReactionToClothing : IEvaluateOnce
    {
        public Dictionary<ClothingOption, AmberReaction> _reactions;
        public override void Run()
        {
            _reactions[StoryDatastore.Instance.ChosenClothing.Value].PerformReaction();
        }
    }
}
