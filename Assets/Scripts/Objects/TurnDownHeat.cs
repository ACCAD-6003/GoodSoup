using System.Collections;
using UnityEngine;

public class TurnDownHeat : Interaction
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
        StartCoroutine(DecreaseHeat());
    }

    IEnumerator DecreaseHeat()
    {
        while (data.BurnerHeat.Value > Globals.PREFERABLE_HEAT) {
            data.BurnerHeat.Value -= 1f;
            yield return new WaitForSeconds(1/10f);
        }
        IsInProgress = false;
    }
}