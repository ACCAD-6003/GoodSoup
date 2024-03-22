using System.Collections;
using UnityEngine;

public class TurnUpHeat : Interaction
{
    StoryDatastore data;

    public void Start()
    {
        data = FindObjectOfType<StoryDatastore>();
    }

    // Update is called once per frame
    override public void DoAction() {
        if (IsInProgress)
        {
            return;
        }

        IsInProgress = true;
        data.BurnerHeat.Value += 10;
        IsInProgress = false;
    }
}
