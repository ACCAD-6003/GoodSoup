using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    interface InteractableObject
    {
        public event Action OnActionStarted;
        public event Action OnActionEnding;

        public void DoAction();
    }
}
