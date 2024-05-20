using UnityEngine;
using static StoryDatastore;

public class SkipIfMoveObject : ISkipCondition
{
    public bool _necessaryValueToSkip;
    public int MoveObjectIndex;
    public override bool ShouldSkip()
    {
        if (Instance == null)
        {
            Debug.LogError("Instance is null in skipifstorydatastorestate.cs");
        }
        return _necessaryValueToSkip == Instance.MoveObjects[MoveObjectIndex].Value;
    }
}