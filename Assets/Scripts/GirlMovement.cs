using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Move,
    Fiddle,
    Express
}

public class GirlMovement : MonoBehaviour
{
    public event Action<string, Vector2> GoalPositionReached;
    public event Action<AnimationType> AnimationStep;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
