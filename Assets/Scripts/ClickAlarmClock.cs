using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAlarmClock : Interaction
{
    public override void LoadData(StoryDatastore data)
    {

    }

    public override void SaveData(StoryDatastore data)
    {

    }

    // Update is called once per frame
    public override void DoAction()
    {
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

        EndAction();
    }
}
