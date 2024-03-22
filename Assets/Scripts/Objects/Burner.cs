using System.Collections;
using UnityEngine;

public class Burner : InteractableObject
{
    StoryDatastore data;

    public void Start()
    {
        data = FindObjectOfType<StoryDatastore>();
    }

    // Update is called once per frame
    override
    public void DoAction() {
        if (IsInProgress)
        {
            return;
        }

        IsInProgress = true;
        data.BurnerHeat.Value += 10;
        Debug.Log(data.BurnerHeat.Value);
        IsInProgress = false;
    }
}
