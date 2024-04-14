using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : Interaction
{
    [SerializeField] GameObject beforeMoved, afterMoved;
    StoryData<bool> _moved;
    public override void LoadData(StoryDatastore data)
    {
        if (data.MoveObjects.ContainsKey(interactionId))
        {
            _moved = data.MoveObjects[interactionId];
        }
        else 
        {
            _moved = new StoryData<bool>(false);
            data.MoveObjects.Add(interactionId, _moved);
        }
        RefreshObjects();
    }

    public override void SaveData(StoryDatastore data)
    {
        data.MoveObjects[interactionId] = _moved;
    }
    void RefreshObjects() {
        beforeMoved.SetActive(!_moved.Value);
        afterMoved.SetActive(_moved.Value);
    }
    public override void DoAction()
    {
        _moved.Value = true;
        StoryDatastore.Instance.MoveObjects[interactionId].Value = true;
        RefreshObjects();
        EndAction();
    }
}
