using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class InteractableObject : MonoBehaviour
{
    public event Action OnActionStarted;
    public event Action OnActionEnding;

    public bool IsInProgress {
        protected set {
            if (value == IsInProgress)
            {
                return;
            }

            if (value)
            {
                OnActionStarted?.Invoke();
            }
            else
            {
                OnActionEnding?.Invoke();
            }
        }

        get {
            return IsInProgress;
        }
    }

    public abstract void DoAction();
}