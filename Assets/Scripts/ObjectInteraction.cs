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

    float searchDistance = 100f;

    bool CanInteractWith(InteractableObject o)
    {
        // check that it's not within amber sightlines
        return true;
    }

    InteractableObject? GetTarget(CallbackContext c)
    {
        Ray mouseTarget = PlayerCamera.ScreenPointToRay(Input.mousePosition);

        bool didHit = Physics.Raycast(mouseTarget, out RaycastHit hitInfo, searchDistance);

        if (!didHit)
        {
            return null;
        }

        hitInfo.transform.TryGetComponent<InteractableObject>(out InteractableObject obj);

        return obj;
    }

    [SerializeField]
    BouncingObject a;

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
                targettedObject.DoAction();
            }
        };

        interactions.Interactions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
