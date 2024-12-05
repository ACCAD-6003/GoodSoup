using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    public static List<float> TimeIntervals = new List<float>() { 1f, 1.5f, 2f };
	[SerializeField] private List<Sprite> _stopwatchSprites;
	[SerializeField] private Image _stopwatchImage;
	public void ChangeSpeed()
	{
		StoryDatastore.Instance.GameTimeSpeedIndex = (StoryDatastore.Instance.GameTimeSpeedIndex + 1) % TimeIntervals.Count;
		RefreshStopwatch();
	}
	private void Awake()
	{
		RefreshStopwatch();
	}
	public void RefreshStopwatch() {
		var timeScale = TimeIntervals[StoryDatastore.Instance.GameTimeSpeedIndex];
		_stopwatchImage.sprite = _stopwatchSprites[StoryDatastore.Instance.GameTimeSpeedIndex];
		Time.timeScale = timeScale;
	}
}
