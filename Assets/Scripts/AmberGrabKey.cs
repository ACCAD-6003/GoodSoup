using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberGrabKey : Interaction
{
    public override void DoAction()
    {
        Destroy(gameObject);
    }

    public override void LoadData(StoryDatastore data)
    {

    }

    public override void SaveData(StoryDatastore data)
    {

    }

    void Awake()
    {
        if (StoryDatastore.Instance.AmberPickedUpKey.Value) {
            Destroy(gameObject);
        }
    }


}
