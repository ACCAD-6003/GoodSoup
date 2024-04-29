using Sirenix.OdinInspector;
using UnityEngine;

public abstract class ISkipCondition : SerializedMonoBehaviour
{
    public abstract bool ShouldSkip();
}