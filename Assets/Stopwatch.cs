using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : Interaction
{
    public List<float> TimeIntervals;
    public TextMeshPro text;
    private int index;

    public override void DoAction()
    {
        index = (index + 1) % TimeIntervals.Count;
        var timeScale = TimeIntervals[index];
        text.text = $"{timeScale}x";
        StoryDatastore.Instance.GameTimeSpeed = timeScale;
        Time.timeScale = timeScale;
        EndAction();
    }

    public override void LoadData(StoryDatastore data)
    {

    }

    public override void SaveData(StoryDatastore data)
    {

    }
}
