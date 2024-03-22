using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class InteractableObject : MonoBehaviour
{
    public event Action OnActionStarted;
    public event Action OnActionEnding;

    private bool _isInProgress;
    public bool IsInProgress {
        get { 
            return _isInProgress;
        }
        protected set {
            if (value == _isInProgress)
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

            _isInProgress = value;
        }
    }

    public abstract void DoAction();
}