using BehaviorTree;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
public class WrapperNode : Node
{
    public ISkipCondition _skipCondition;
    public bool _shouldEvaluteOnlyOnce = false;
    public List<Node> nodes;
    bool _canSkip = true;
    bool _skipEverytime = false;

    public override NodeState Evaluate()
    {
        if (_skipEverytime) {
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
        if (_canSkip) {
            if (_shouldEvaluteOnlyOnce) {
                _canSkip = false;
            }
            if (_skipCondition.ShouldSkip())
            {
                if (_shouldEvaluteOnlyOnce) {
                    _skipEverytime = true;
                }
                state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
        }
        foreach (Node node in nodes)
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
