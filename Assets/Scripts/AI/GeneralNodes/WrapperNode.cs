﻿using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class SkipIfStoryDatastoreState<T> : ISkipCondition {
    StoryData<T> _storyData;
    T _necessaryValueToSkip;
    bool _skipIfDNE = false;
    public SkipIfStoryDatastoreState(StoryData<T> storyData, T necessaryValueToSkip) {
        _storyData = storyData;
        _necessaryValueToSkip = necessaryValueToSkip;
    }
    public SkipIfStoryDatastoreState(StoryData<T> storyData, T necessaryValueToSkip, bool skipIfDNE)
    {
        _storyData = storyData;
        _necessaryValueToSkip = necessaryValueToSkip;
        _skipIfDNE = skipIfDNE;
    }
    public bool ShouldSkip()  => _skipIfDNE ? !_storyData.Value.Equals(_necessaryValueToSkip) : _storyData.Value.Equals(_necessaryValueToSkip);
}
public interface ISkipCondition
{
    public bool ShouldSkip();
}
public class WrapperNode : Node
{
    private ISkipCondition _skipCondition;
    public WrapperNode(ISkipCondition skipCondition, List<Node> nodes) : base(nodes) {
        _skipCondition = skipCondition;

    }
    public override NodeState Evaluate()
    {
        if (_skipCondition.ShouldSkip()) {
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return NodeState.RUNNING;
                default:
                    state = NodeState.SUCCESS;
                    return NodeState.SUCCESS;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}