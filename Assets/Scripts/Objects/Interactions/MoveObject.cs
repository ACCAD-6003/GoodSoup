using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : Interaction
{
    [SerializeField] GameObject beforeMoved, afterMoved;
    public StoryData<bool> _moved;
    public AudioSource _optionalSrc;
    public AudioClip _optionalSoundForBeforeMovedToAfter;
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
        Debug.Log("SETTING BEFORE MOVED ACTIVE : " + !_moved.Value + " AND AFTER MOVED ACTIVE : " + _moved.Value);
        beforeMoved.SetActive(!_moved.Value);
        afterMoved.SetActive(_moved.Value);
    }
    public override void DoAction()
    {
        Debug.Log("OLD VALUE : " + _moved.Value + " NEW VALUE : " + !_moved.Value);
        _moved.Value = !_moved.Value;
        if (_optionalSrc != null && _moved.Value) {
            _optionalSrc.PlayOneShot(_optionalSoundForBeforeMovedToAfter);
        }
        StoryDatastore.Instance.MoveObjects[interactionId].Value = _moved.Value;
        RefreshObjects();
        EndAction();
    }

}
