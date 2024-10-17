using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : Interaction
{
    public static List<float> TimeIntervals = new List<float>() { 1f, 1.5f, 2f };
    public TextMeshPro text;

	public override void DoAction()
	{
		StoryDatastore.Instance.GameTimeSpeedIndex = (StoryDatastore.Instance.GameTimeSpeedIndex + 1) % TimeIntervals.Count;
		RefreshStopwatch();
		EndAction();
	}
	private void Awake()
	{
		RefreshStopwatch();
	}
	public void RefreshStopwatch() {
		var timeScale = TimeIntervals[StoryDatastore.Instance.GameTimeSpeedIndex];
		// Format timeScale as x.x
		text.text = $"{timeScale:F1}X";
		Time.timeScale = timeScale;
	}
	public override void LoadData(StoryDatastore data)
    {

    }

    public override void SaveData(StoryDatastore data)
    {

    }
}
