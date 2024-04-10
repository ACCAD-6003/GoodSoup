using BehaviorTree;

public class UpdateAmberLookRot : Node {
    grid_manager _grid;
    Interaction _intObj;
    bool set = false;
    public UpdateAmberLookRot(grid_manager grid, Interaction intObj) {
        _grid = grid;
        _intObj = intObj;
    }
    public override NodeState Evaluate()
    {
        if (!set)
        {
            _grid.SetLookRot(_intObj.AssociatedDirection);
            set = true;
        }
        state = NodeState.SUCCESS;
        return NodeState.SUCCESS;
    }
}