
using BehaviorTree;
public class WaitForStoryDataChange : Node
{
    public ISkipCondition _condition;
    private bool skipped = false;
    public override NodeState Evaluate()
    {
        if (skipped)
        {
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
        if (!skipped && _condition.ShouldSkip())
        {
            skipped = true;
            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
        state = NodeState.FAILURE;
        return NodeState.FAILURE;
    }

}