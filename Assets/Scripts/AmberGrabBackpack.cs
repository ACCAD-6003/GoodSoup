using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberGrabBackpack : Interaction
{
    [SerializeField] GameObject _backpackInWorld;
    public override void LoadData(StoryDatastore data)
    {
        _backpackInWorld.SetActive(!StoryDatastore.Instance.PickedUpBackpack.Value);
    }

    public override void SaveData(StoryDatastore data)
    {

    }

    public override void DoAction()
    {
        StoryDatastore.Instance.PickedUpBackpack.Value = !StoryDatastore.Instance.PickedUpBackpack.Value;
        _backpackInWorld.SetActive(!StoryDatastore.Instance.PickedUpBackpack.Value);
    }
}
