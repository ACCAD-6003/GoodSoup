using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : Interaction
{
    [SerializeField] GameObject beforeMoved, afterMoved;
    public StoryData<bool> _moved;
    public AudioSource _optionalSrc;
    public AudioClip _optionalSoundForBeforeMovedToAfter;
    public bool putBackAfterTime = false;
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
        _moved.Value = !_moved.Value;
        if (_optionalSrc != null && _moved.Value) {
            _optionalSrc.PlayOneShot(_optionalSoundForBeforeMovedToAfter);
        }
        StoryDatastore.Instance.MoveObjects[interactionId].Value = _moved.Value;
        RefreshObjects();
        if (!putBackAfterTime) {
            EndAction();
            return;
        }
        StartCoroutine(PutBack());
    }
    IEnumerator PutBack() {
        yield return new WaitForSeconds(0.5f);
        _moved.Value = !_moved.Value;
        StoryDatastore.Instance.MoveObjects[interactionId].Value = _moved.Value;
        RefreshObjects();
        EndAction();
    }

}
