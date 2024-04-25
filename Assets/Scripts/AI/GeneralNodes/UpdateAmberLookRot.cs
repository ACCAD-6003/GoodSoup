using BehaviorTree;
using UnityEngine;

public class UpdateAmberLookRot : Node {
    grid_manager _grid;
    Vector3 _dir;
    bool set = false;
    public UpdateAmberLookRot(grid_manager grid, Interaction intObj) {
        _grid = grid;
        _dir = intObj.AssociatedDirection;
    }
    public UpdateAmberLookRot(grid_manager grid, Vector3 dir)
    {
        _grid = grid;
        _dir = dir;
    }
    public override NodeState Evaluate()
    {
        if (!set)
        {
            _grid.char_s.SetArbitraryRot(_dir);
            set = true;
        }
        state = NodeState.SUCCESS;
        return NodeState.SUCCESS;
    }
}