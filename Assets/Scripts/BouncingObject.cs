using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObject : MonoBehaviour, InteractableObject
{
    public event Action OnActionStarted;
    public event Action OnActionEnding;
    public bool IsInProgress { private set; get; }

    // Update is called once per frame
    public void DoAction() {
        if (IsInProgress)
        {
            return;
        }

        IsInProgress = true;
        OnActionStarted?.Invoke();

        StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        Vector3 initialPosition = transform.position;
        float impulseVelocity = 5f;
        float g = Physics.gravity.y;

        float endTime = impulseVelocity / -g;

        for (float t = 0; t < endTime; t += Time.deltaTime)
        {
            transform.position = initialPosition 
                + Vector3.up * (impulseVelocity * t + g * t * t);
            yield return new WaitForEndOfFrame();
        }

        OnActionEnding?.Invoke();
        IsInProgress = false;
    }
}
