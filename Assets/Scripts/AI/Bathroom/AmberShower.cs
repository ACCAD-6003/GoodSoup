using Assets.Scripts.Objects.Interactions;
using Assets.Scripts.UI;
using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberShower : Node
{
    bool startedShowering = false;
    bool finishedShowering = false;
    public AmberUseShower _shower;
    public override NodeState Evaluate()
    {
        if (!startedShowering) {
            startedShowering = true;
            _shower.StartAction();
        }
        if (finishedShowering) {
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
        if (StoryDatastore.Instance.DoneShowering.Value)
        {
            finishedShowering = true;
            _shower.TurnOffShower();
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;
    }
}