using System.Collections;
using UnityEngine;

public class TurnUpHeat : Interaction
{
    StoryDatastore data;

    public override void LoadData(StoryDatastore data)
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        data = FindObjectOfType<StoryDatastore>();
    }

    // Update is called once per frame
    override protected void DoAction() {
        data.BurnerHeat.Value += 10;
        EndAction();
    }
}
