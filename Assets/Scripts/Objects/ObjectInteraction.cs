using Assets.Scripts.Objects.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField]
    public Transform Amber;

    [SerializeField]
    float amberSightAngle = 40;

    [SerializeField]
    AudioSource playerInteractSoundSource;

    [SerializeField]
    AudioClip playerInteractBlip;

    public bool PopUpOpened = false;
    #nullable enable

    float searchDistance = 100f;

    public bool IsInAmberSightlines(InteractableObject o)
    {
        Vector3 amberFacing = Amber.forward;
        Vector3 fromAmberToObject = o.transform.position - Amber.position;

        float angle = Vector3.Angle(amberFacing, fromAmberToObject);

        return angle < amberSightAngle;
    }

    bool CanInteractWith(InteractableObject o)
    {
        if (o.PlayerInteraction == null || PopUpOpened)
        {
            return false;
        }

        return (!o.PlayerInteraction.IsInProgress && !IsInAmberSightlines(o)) || o.CanInteractWhileAmberLooking || o.TryGetComponent(out PopUp _);
    }

    InteractableObject? GetTarget(CallbackContext c)
    {
        Ray mouseTarget = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool didHit = Physics.Raycast(mouseTarget, out RaycastHit hitInfo, searchDistance, ~(1<<8));

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

    void Awake()
    {
        if (Amber == null)
        {
            Amber = GameObject.FindGameObjectWithTag("Body").transform;
        }
        TurnOffOutlines();
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
                playerInteractSoundSource.PlayOneShot(playerInteractBlip);
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
