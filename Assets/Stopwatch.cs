using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    public static List<float> TimeIntervals = new List<float>() { 1f, 1.5f, 2f };
	[SerializeField] private List<Sprite> _stopwatchSprites;
	[SerializeField] private Sprite _stopwatchNotUnlocked;
	[SerializeField] private Image _stopwatchImage;
	public void ChangeSpeed()
	{
		if (!Globals.StopwatchUnlocked) return;
		StoryDatastore.Instance.GameTimeSpeedIndex = (StoryDatastore.Instance.GameTimeSpeedIndex + 1) % TimeIntervals.Count;
		RefreshStopwatch();
	}
	private void Awake()
	{
		RefreshStopwatch();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		RefreshStopwatch();
	}
	public void RefreshStopwatch() {
		var timeScale = TimeIntervals[StoryDatastore.Instance.GameTimeSpeedIndex];

		if (Globals.StopwatchUnlocked)
		{
			_stopwatchImage.sprite = _stopwatchSprites[StoryDatastore.Instance.GameTimeSpeedIndex];
		}
		else 
		{ 
			_stopwatchImage.sprite = _stopwatchNotUnlocked;
		}
		Time.timeScale = timeScale;
	}
}
