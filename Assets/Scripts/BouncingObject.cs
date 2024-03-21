using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObject : InteractableObject
{
    public override event Action OnActionStarted;
    public override event Action OnActionEnding;

    // Update is called once per frame
    override
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

        for (float t = 0; t <= endTime; t += Time.deltaTime)
        {
            transform.position = initialPosition 
                + Vector3.up * (impulseVelocity * t + g * t * t);
            yield return new WaitForEndOfFrame();
        }

        transform.position = initialPosition;

        OnActionEnding?.Invoke();
        IsInProgress = false;
    }
}
