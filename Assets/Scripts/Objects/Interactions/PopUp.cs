﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Interactions
{
    internal class PopUp : Interaction
    {
        [SerializeField] GameObject popUp;
        bool poppedUp = false;
        public override void DoAction()
        {
            poppedUp = !poppedUp;
            popUp.SetActive(poppedUp);
            Time.timeScale = poppedUp ? 0 : 1;
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