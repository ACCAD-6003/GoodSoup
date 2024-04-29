using BehaviorTree;
using UnityEngine;

public class UpdateAmberLookRot : IEvaluateOnce {
    public grid_manager _grid;
    public Vector3 _dir;
    public override void Run()
    {
        _grid.char_s.SetArbitraryRot(_dir);
    }
}