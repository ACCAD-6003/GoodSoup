using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class IAmberReaction
{
    public abstract void PerformReaction();
}  

namespace Assets.Scripts.AI.Bedroom
{

    public class AmberReactionToClothing : IEvaluateOnce
    {
        public Dictionary<ClothingOption, IAmberReaction> _reactions;
        public StoryData<ClothingOption> _storyData;
        public override void Run()
        {
            _reactions[_storyData.Value].PerformReaction();
        }
    }
}
