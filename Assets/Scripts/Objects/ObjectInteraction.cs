using Assets.Scripts.Objects.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private System.Action<CallbackContext> interactAction;
    private System.Action<CallbackContext> hoverAction;

    private GhostInput interactions;

    public bool IsInAmberSightlines(InteractableObject o)
    {
        Transform viewCone = Amber.gameObject.GetComponentInChildren<FuckedUpLightCone>().transform;

        Vector3 amberFacing = -viewCone.forward;
        Vector3 fromAmberToObject = o.transform.position - viewCone.position;

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
            var amber = GameObject.FindGameObjectWithTag("Body");
            if (amber != null) {
                Amber = amber.transform;
            }
        }
        TurnOffOutlines();
    }
    
    private void Start()
    {
        interactions = new();

        interactAction = (CallbackContext c) =>
        {
            InteractableObject targettedObject = GetTarget(c);

            if (targettedObject != null && CanInteractWith(targettedObject))
            {
                targettedObject.PlayerInteraction.StartAction();
                playerInteractSoundSource.PlayOneShot(playerInteractBlip);
            }
        };

        hoverAction = (CallbackContext c) =>
        {
            InteractableObject targettedObject = GetTarget(c);

            TurnOffOutlines();

            if (targettedObject != null && CanInteractWith(targettedObject))
            {
                targettedObject.GetComponent<Outline>().enabled = true;
            }
        };

        interactions.Interactions.Interact.performed += interactAction;
        interactions.Interactions.Hover.performed += hoverAction;

        interactions.Interactions.Enable();
    }

    private void OnDestroy()
    {
        interactions.Interactions.Interact.performed -= interactAction;
        interactions.Interactions.Hover.performed -= hoverAction;
    }
}
