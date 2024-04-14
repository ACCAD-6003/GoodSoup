using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [Tooltip("Optional ID used for some objects for persistence")]
    public int interactionId;

    public event Action OnActionStarted;
    public event Action OnActionEnding;
    public bool singleUse = false;
    public bool usedUp = false;

    public Vector3 AssociatedDirection = Vector3.forward;

    public bool isPlayer = true;

    [SerializeField]
    private bool _isInProgress;
    private void Start()
    {
        LoadData(StoryDatastore.Instance);
    }
    public abstract void LoadData(StoryDatastore data);
    public abstract void SaveData(StoryDatastore data);

    public bool IsInProgress
    {
        get
        {
            return _isInProgress || usedUp;
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
    public void PutInProgress() {
        if (IsInProgress)
        {
            return;
        }

        IsInProgress = true;
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

    public abstract void DoAction();

    public void EndAction()
    {
        IsInProgress = false;
        if (singleUse) {
            usedUp = true;
        }
    }
}
