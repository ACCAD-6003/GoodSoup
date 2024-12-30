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

	[Tooltip("Sound that plays when interaction is performed. Requires audiosource.")]
	[SerializeField]
	public AudioClip interactionSound;
	[Range(0.0f, 1f)]
	public float interactionVol = 0.5f;
	public bool toggleSound = false;

	[DoNotSerialize]
	bool toggled = false;

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

	[SerializeField, Range(0.1f, 5f)]
	private float fadeDuration = 1f;

	private Coroutine fadeCoroutine;

	private void Start()
	{
		LoadData(StoryDatastore.Instance);
		if (interactionSound)
		{
			_audioSource = gameObject.AddComponent<AudioSource>();
			if (toggleSound)
			{
				_audioSource.loop = true;
				_audioSource.volume = 0; // Start with volume at 0 for toggled state.
			}
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
	public void PutInProgress()
	{
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

		if (_audioSource)
		{
			if (toggleSound)
			{
				toggled = !toggled;

				if (fadeCoroutine != null)
				{
					StopCoroutine(fadeCoroutine);
				}

				if (toggled)
				{
					_audioSource.clip = interactionSound;
					_audioSource.volume = 0; // Start fade-in from 0 volume.
					_audioSource.Play();
					fadeCoroutine = StartCoroutine(FadeAudioVolume(_audioSource, interactionVol, fadeDuration));
				}
				else
				{
					fadeCoroutine = StartCoroutine(FadeAudioVolume(_audioSource, 0, fadeDuration, () => _audioSource.Stop()));
				}
			}
			else
			{
				_audioSource.PlayOneShot(interactionSound, interactionVol);
			}
		}

		IsInProgress = true;

		DoAction();
	}

	public abstract void DoAction();

	public void EndAction()
	{
		IsInProgress = false;
		if (singleUse)
		{
			usedUp = true;
		}
	}

	private IEnumerator FadeAudioVolume(AudioSource source, float targetVolume, float duration, Action onComplete = null)
	{
		float startVolume = source.volume;
		float elapsedTime = 0;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			source.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
			yield return null;
		}

		source.volume = targetVolume;
		onComplete?.Invoke();
	}
}
