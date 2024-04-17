using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRecipeSequence : Interaction
{
    [SerializeField] GameObject closedDoor, openDoor, recipeInDoor, floatingRecipe, recipeOnTable, splinesInScene;
    [SerializeField] SplineFollower _follower, _bookOnTable;
    [SerializeField] GameObject _splinesPrefab;
    [SerializeField] List<RotateFan> rotateFans;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip hit, whoosh;
    StoryData<bool> _moved;
    bool _recipeGone = false;
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
        closedDoor.SetActive(!_moved.Value);
        openDoor.SetActive(_moved.Value);
    }
    void OnNodePassed(List<SplineTracer.NodeConnection> passed)
    {
        var node = passed[0];
        int index = 0;

        if (node.node.gameObject.name == "Fan1Junction")
        {
            index = GetIndexFromOrientationFan1(rotateFans[0].orientationIndex);
            if (index == 2 || index == 0) {
                src.PlayOneShot(whoosh);
            }
        }
        else if (node.node.gameObject.name == "Fan2Junction")
        {
            index = GetIndexFromOrientationFan2(rotateFans[1].orientationIndex);
            if (index == 2 || index == 0)
            {
                src.PlayOneShot(whoosh);
            }
        }
        else if (node.node.gameObject.name == "WON")
        {
            recipeOnTable.SetActive(true);
            Destroy(recipeInDoor);
            Destroy(floatingRecipe);
            _recipeGone = true;
            _bookOnTable.enabled = (true);
            src.PlayOneShot(hit);
            src.PlayOneShot(whoosh);
            EndAction();
            return;
        }
        else if (node.node.gameObject.name == "Reset")
        {
            src.PlayOneShot(hit);
            ResetPuzzleSequence();
            return;
        }
        else
        {
            Debug.Log("No node name matches");
            return;
        }
/*        if (index == 0) {

            _follower.spline = node.node.GetConnections()[index].spline;
        }*/

        if (index != 0) {
            _follower.SetPercent(0f);
            _follower.spline = node.node.GetConnections()[index].spline;
            _follower.SetPercent(0.01f);
        }
    }
    private void ResetPuzzleSequence()
    {
        _follower.onNode -= OnNodePassed;
        Debug.Log("Resetting puzzle sequence...");

        var gameObj = _follower.gameObject;
        Destroy(_follower);
        _follower = gameObj.AddComponent<SplineFollower>();

        Destroy(splinesInScene);
        splinesInScene = Instantiate(_splinesPrefab);

        _follower.spline = GameObject.FindGameObjectWithTag("RecipeComputer").GetComponent<SplineComputer>();

        _follower.SetPercent(0f);

        _follower.follow = true;

        _moved.Value = !_moved.Value;
        StoryDatastore.Instance.MoveObjects[interactionId].Value = _moved.Value;
        RefreshObjects();
        foreach (var fan in rotateFans)
        {
            fan.EndAction();
        }
        EndAction();
    }
    // bad bad bad not good terrible ugly i know how to do this better but i am tired and it works fuck off paige this is only ever going to be used once
    int GetIndexFromOrientationFan1(int orientation) {
        int index = 0;
        switch (orientation)
        {
            case 2:
                index = 2;
                break;
            case 3:
                index = 0;
                break;
            default:
                index = 1;
                break;
        }
        return index;
    }
    // bad bad bad not good terrible ugly i know how to do this better but i am tired and it works fuck off paige this is only ever going to be used once
    int GetIndexFromOrientationFan2(int orientation)
    {
        int index = 0;
        switch (orientation)
        {
            case 2:
                index = 2;
                break;
            case 1:
                index = 0;
                break;
            default:
                index = 1;
                break;
        }
        return index;
    }
    /// <summary>
    ///  this library is fucking broken!!!!! this is how you have to do this?!?!??!
    /// </summary>
    public override void DoAction()
    {
        Debug.Log("OLD VALUE : " + _moved.Value + " NEW VALUE : " + !_moved.Value);
        _moved.Value = !_moved.Value;
        StoryDatastore.Instance.MoveObjects[interactionId].Value = _moved.Value;
        RefreshObjects();

        if (!_recipeGone)
        {
            foreach (var fan in rotateFans)
            {
                fan.PutInProgress();
            }
            _follower.spline = GameObject.FindGameObjectWithTag("RecipeComputer").GetComponent<SplineComputer>();
            _follower.SetPercent(0f);
            _follower.followSpeed = 4f;
            _follower.onNode += OnNodePassed;
        }
        else
        {
            EndAction();
        }
    }

}
