using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public event Action OnActionStarted;
    public event Action OnActionEnding;
    public bool singleUse = false;

    public Vector3 AssociatedDirection = Vector3.forward;

    public bool isPlayer = true;

    [SerializeField]
    private bool _isInProgress;

    public bool IsInProgress
    {
        get
        {
            return _isInProgress;
        }
        private set
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

    public void StartAction()
    {
        if (IsInProgress)
        {
            return;
        }

        IsInProgress = true;

        DoAction();
    }

    protected abstract void DoAction();

    protected void EndAction()
    {
        IsInProgress = false;
        if (singleUse) {
            Destroy(this);
        }
    }
}
