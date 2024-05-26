using System;
using System.Collections;
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
        ObjectInteraction objInteraction;
        private void Awake()
        {
            objInteraction = FindObjectOfType<ObjectInteraction>();
        }
        public override void DoAction()
        {
            if (objInteraction.PopUpOpened && !poppedUp)
            {
                return;
            }
            poppedUp = !poppedUp;
            popUp.SetActive(poppedUp);
            // will need to change based on new stopwatch
            Time.timeScale = poppedUp ? 0 : 1;
            Debug.Log("Popped up");
            objInteraction.PopUpOpened = poppedUp;
            Debug.Log($"Set pop up opened to {poppedUp}");
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
