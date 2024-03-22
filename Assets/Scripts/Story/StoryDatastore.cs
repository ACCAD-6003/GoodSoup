using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDatastore : MonoBehaviour
{
    [SerializeField]
    public StoryData<float> BurnerHeat;

    public void Awake()
    {
        BurnerHeat = new(100f);
    }
}
