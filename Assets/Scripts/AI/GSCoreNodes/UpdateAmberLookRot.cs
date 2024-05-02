using Assets.Scripts.AI.Kitchen;
using BehaviorTree;
using UnityEngine;

public class UpdateAmberLookRot : IEvaluateOnce {
    public grid_manager _grid;
    public MoveDir _dir;
    public override void Run()
    {
        _grid.char_s.SetArbitraryRot(MoveToTile.MoveDirections[_dir]);
    }
}