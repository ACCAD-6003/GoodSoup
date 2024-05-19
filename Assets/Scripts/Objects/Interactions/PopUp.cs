using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Interactions
{
    public class PopUp : Interaction
    {
        [SerializeField] GameObject popUp;
        bool poppedUp = false;
        public override void DoAction()
        {
            poppedUp = !poppedUp;
            popUp.SetActive(poppedUp);
            // will need to change based on new stopwatch
            Time.timeScale = poppedUp ? 0 : 1;
            var objectInteraction = FindObjectOfType<ObjectInteraction>();
            objectInteraction.PopUpOpened = poppedUp;
            EndAction();
        }

        public override void LoadData(StoryDatastore data)
        {

        }

        public override void SaveData(StoryDatastore data)
        {

        }
    }
}
