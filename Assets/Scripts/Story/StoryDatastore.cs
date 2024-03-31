using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDatastore : MonoBehaviour
{
    [SerializeField]
    public StoryData<bool> AnyBookDropped;
    [SerializeField]
    public StoryData<float> BurnerHeat;
    [SerializeField]
    public StoryData<bool> CurtainsOpen;
}
