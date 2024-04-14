using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MirrorState { NOT_FOGGED, FOGGED, DRAWN_ON }
public class DrawOnMirror : Interaction
{
    [SerializeField] Material fogged, not_fogged, drawn_on;
    private void Start()
    {
        RefreshMirror();
        if (StoryDatastore.Instance.MirrorState.Value != MirrorState.FOGGED)
        {
            base.usedUp = true;
        }
    }
    void Update()
    {
        if (StoryDatastore.Instance.MirrorState.Value == MirrorState.NOT_FOGGED) {
            if (StoryDatastore.Instance.HotShowerDuration.Value >= 2f) {
                StoryDatastore.Instance.MirrorState.Value = MirrorState.FOGGED;
                base.usedUp = false;
                RefreshMirror();
            }
        }
    }
    void RefreshMirror() {
        Material mat;
        switch (StoryDatastore.Instance.MirrorState.Value)
        { 
            case MirrorState.NOT_FOGGED:
                mat = not_fogged;
                break;
            case MirrorState.DRAWN_ON:
                mat = drawn_on;
                break;
            default:
                mat = fogged;
                break;
        }
        GetComponent<Renderer>().material = mat;
    }
    public override void LoadData(StoryDatastore data)
    {

    }

    public override void SaveData(StoryDatastore data)
    {
        // reference so no body necessary
    }

    public override void DoAction()
    {
        if (StoryDatastore.Instance.MirrorState.Value == MirrorState.FOGGED)
        {
            StoryDatastore.Instance.MirrorState.Value = MirrorState.DRAWN_ON;
            RefreshMirror();
        }
        else {
            StoryDatastore.Instance.MirrorState.Value = MirrorState.FOGGED;
            RefreshMirror();
        }
        EndAction();
    }
}
