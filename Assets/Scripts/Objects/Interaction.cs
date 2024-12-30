using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [Tooltip("Optional ID used for some objects for persistence")]
    [SerializeField]
    public int interactionId;

    [Tooltip("Sound that plays when interaction is performed. Requires audiosou")]
	[SerializeField]
	public AudioClip interactionSound;
	[Range(0.0f, 1f)]
	public float interactionVol = 0.5f;

	public event Action OnActionStarted;
    public event Action OnActionEnding;
	[SerializeField]
	public bool singleUse = false;
    [DoNotSerialize]
    public bool usedUp = false;

    [SerializeField]
    public Vector3 AssociatedDirection = Vector3.forward;

	[SerializeField]
	public bool isPlayer = true;

    [SerializeField]
    private bool _isInProgress;

    [NonSerialized]
    private AudioSource _audioSource;
	private void Start()
    {
        LoadData(StoryDatastore.Instance);
        if (interactionSound) { 
            _audioSource = gameObject.AddComponent<AudioSource>();
		}
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
        Debug.Log("Starting action");
        if (IsInProgress)
        {
            Debug.Log("Action already in progress");
			return;
        }

        if (_audioSource) {
            Debug.Log("Playing sound");
			_audioSource.PlayOneShot(interactionSound, interactionVol);
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
