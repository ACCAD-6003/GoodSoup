using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]
public class VideoController : MonoBehaviour
{
	public VideoPlayer videoPlayer;
	private AudioSource audioSource;
	private float lastTimeScale;

	private void Start()
	{
		if (videoPlayer == null)
		{
			Debug.LogError("VideoPlayer not assigned!");
			return;
		}
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
		videoPlayer.SetTargetAudioSource(0, audioSource);
		videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		lastTimeScale = Time.timeScale;
		videoPlayer.playbackSpeed = Time.timeScale;
	}

	private void Update()
	{
		if (Time.timeScale != lastTimeScale)
		{
			lastTimeScale = Time.timeScale;
			videoPlayer.playbackSpeed = Time.timeScale;
		}
	}
}
