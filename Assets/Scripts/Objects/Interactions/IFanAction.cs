using Sirenix.Serialization.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IFanAction : MonoBehaviour
{
    public abstract void FanAligned();
    public abstract void FanUnaligned();
}
