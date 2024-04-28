using BehaviorTree;

public abstract class IEvaluateOnce : Node
{
    bool evaluated = false;
    public override NodeState Evaluate()
    {
        if (!evaluated)
        {
            Run();
            evaluated = true;
        }
        state = NodeState.SUCCESS;
        return NodeState.SUCCESS;
    }
    public abstract void Run();
}
