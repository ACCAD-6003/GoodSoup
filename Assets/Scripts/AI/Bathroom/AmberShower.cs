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
    Shower _shower;
    public AmberShower(Shower shower) { 
        _shower = shower;
    }
    public override NodeState Evaluate()
    {
        if (!startedShowering) {
            startedShowering = true;
            _shower.StartAction();
        }
        if (StoryDatastore.Instance.DoneShowering.Value)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;
    }
}