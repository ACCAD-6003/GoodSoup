using System.Collections;
using UnityEngine;

public class TurnDownHeat : Interaction
{
    StoryDatastore data;

    public override void LoadData(StoryDatastore data)
    {
        throw new System.NotImplementedException();
    }

    public override void SaveData(StoryDatastore data)
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        data = FindObjectOfType<StoryDatastore>();
    }

    // Update is called once per frame
    public override void DoAction() {
        Debug.Log("Turning down the heat!");
        StartCoroutine(DecreaseHeat());
    }

    IEnumerator DecreaseHeat()
    {
        while (data.BurnerHeat.Value > Globals.PREFERABLE_HEAT) {
            data.BurnerHeat.Value -= 1f;
            yield return new WaitForSeconds(1/10f);
        }
        EndAction();
    }
}
