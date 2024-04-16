using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Objects.Interactions
{
    internal class AmberCheckEmail : Interaction
    {
        public override void LoadData(StoryDatastore data)
        {

        }

        public override void SaveData(StoryDatastore data)
        {

        }

        public override void DoAction()
        {
            switch (StoryDatastore.Instance.EmailState.Value) {
                case ComputerHUD.EmailState.MEAN_EMAIL:
                case ComputerHUD.EmailState.NICE_EMAIL:
                    UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UIElements.BubbleIcon.PARANOID);
                    StoryDatastore.Instance.Paranoia.Value += 1f;
                    break;
                case ComputerHUD.EmailState.MEAN_EMAIL_CONFIRMED:
                    UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UIElements.BubbleIcon.ANNOYANCE);
                    StoryDatastore.Instance.Annoyance.Value += 5f;
                    StoryDatastore.Instance.ChosenEnding.Value = Ending.KICKED_OUT_OF_COLLEGE;
                    SceneManager.LoadScene("");
                    break;
                case ComputerHUD.EmailState.NICE_EMAIL_CONFIRMED:
                    // Change this to relief
                    UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UIElements.BubbleIcon.HAPPY);
                    StoryDatastore.Instance.Happiness.Value += 5f;
                    break;
            }
            EndAction();
        }
    }
}
