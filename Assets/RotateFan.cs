using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFan : Interaction
{
    [SerializeReference] IFanAction action;
    [SerializeField] int orientationIndex;
    [SerializeField] int numberOfOrientations;
    [SerializeField] int orientationToDoFanAction;
    [SerializeField] float offset;
    float rotationAmount = 0f;
    bool fanAligned = false;
    private void Awake()
    {
        rotationAmount = (360 / numberOfOrientations);
    }
    void Start()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate((orientationIndex * new Vector3(0, rotationAmount, 0)) + new Vector3(0, offset,0));
    }
    public override void DoAction()
    {
        StartCoroutine(Bounce());


    }
    IEnumerator Bounce()
    {
        Vector3 initialPosition = transform.position;
        float impulseVelocity = 2f;
        float g = Physics.gravity.y;

        float endTime = impulseVelocity / -g;

        for (float t = 0; t <= endTime; t += Time.deltaTime)
        {
            transform.position = initialPosition
                + Vector3.up * (impulseVelocity * t + g * t * t);
            yield return new WaitForEndOfFrame();
        }

        transform.position = initialPosition;
        transform.Rotate(new Vector3(0, rotationAmount, 0));

        if (++orientationIndex >= numberOfOrientations) {
            orientationIndex = 0;
        }
        if (orientationIndex == orientationToDoFanAction)
        {
            fanAligned = true;
            action.FanAligned();
        }
        else if (fanAligned) {
            fanAligned = false;
            action.FanUnaligned();
        }
        EndAction();
    }

    public override void LoadData(StoryDatastore data)
    {
        // add save load
    }

    public override void SaveData(StoryDatastore data)
    {
        
    }
}
