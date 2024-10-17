using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.AI.CoreNodes
{
	internal class PickSoupEnding : IEvaluateOnce
	{
		public override void Run()
		{
			var chosenEnding = StoryDatastore.Instance.ChosenEnding.Value;
			if (StoryDatastore.Instance.GoodSoupPuzzleSolved.Value && chosenEnding != Ending.BURNT_DOWN)
			{
				if (StoryDatastore.Instance.FoodQuality.Value < 0f)
				{
					StoryDatastore.Instance.ChosenEnding.Value = Ending.MID_SOUP;
					SceneManager.LoadScene("Endings");
				}
				else
				{
					StoryDatastore.Instance.ChosenEnding.Value = Ending.GOOD_SOUP;
					SceneManager.LoadScene("GoodSoupCutscene");
				}
				return;
			}
		}
	}
}
