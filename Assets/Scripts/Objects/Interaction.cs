using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public event Action OnActionStarted;
    public event Action OnActionEnding;

    [SerializeField]
    private bool _isInProgress;

    public bool isPlayer = true;
    public bool IsInProgress
    {
        get
        {
            return _isInProgress;
        }
        protected set
        {
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
