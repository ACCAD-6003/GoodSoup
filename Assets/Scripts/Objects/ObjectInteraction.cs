using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField]
    Camera PlayerCamera;

    [SerializeField]
    Transform Amber;

    [SerializeField]
    float amberSightAngle = 40;

#nullable enable

    float searchDistance = 100f;

    private Vector3 toXZ(Vector3 v)
    {
        return Vector3.Scale(Vector3.one - Vector3.up, v);
    }

    bool CanInteractWith(InteractableObject o)
    {
        // check that it's not within amber sightlines
        Vector3 amberFacing = toXZ(Amber.rotation * Vector3.forward);
        Vector3 fromAmberToObject = toXZ(o.transform.position - Amber.position);

        float angle = Vector3.Angle(amberFacing, fromAmberToObject);

        return o.PlayerInteraction != null && angle > amberSightAngle;
    }

    InteractableObject? GetTarget(CallbackContext c)
    {
        Ray mouseTarget = PlayerCamera.ScreenPointToRay(Input.mousePosition);

        bool didHit = Physics.Raycast(mouseTarget, out RaycastHit hitInfo, searchDistance);

        if (!didHit)
        {
            return null;
        }

        return hitInfo.transform.GetComponentInParent<InteractableObject>();
    }

    void TurnOffOutlines()
    {
        foreach (Outline o in FindObjectsOfType<Outline>()) {
            o.enabled = false;
        }
    }

    private void Start()
    {
        GhostInput interactions = new();

        interactions.Interactions.Interact.performed += (CallbackContext c) =>
        {
            InteractableObject? targettedObject = GetTarget(c);

            if (
                targettedObject != null 
                && CanInteractWith(targettedObject)
            )
            {
                targettedObject.PlayerInteraction.StartAction();
            }
        };

        interactions.Interactions.Hover.performed += (CallbackContext c) =>
        {
            InteractableObject? targettedObject = GetTarget(c);

            TurnOffOutlines();

            if (
                targettedObject != null
                && CanInteractWith(targettedObject)
            )
            {
                targettedObject.GetComponent<Outline>().enabled = true;
            }
        };

        interactions.Interactions.Enable();
    }
}
