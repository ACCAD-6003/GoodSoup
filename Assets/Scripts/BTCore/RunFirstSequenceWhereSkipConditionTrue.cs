using BehaviorTree;
using System.Collections.Generic;

public class RunFirstSequenceWhereSkipConditionTrue : Node
{
    public Dictionary<ISkipCondition, Sequence> Sequences;
    Sequence sequenceWeAreRunning = null;
    public override NodeState Evaluate()
    {
        if (sequenceWeAreRunning == null)
        {
            foreach (var sequence in Sequences)
            {
                if (sequence.Key.ShouldSkip())
                {
                    sequenceWeAreRunning = sequence.Value;
                    break;
                }
            }
            state = NodeState.RUNNING;
            return NodeState.RUNNING;
        }
        else
        {
            state = sequenceWeAreRunning.Evaluate();
            return state;
        }
    }

}