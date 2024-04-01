using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    public tile AssociatedTile;

    [NonSerialized]
    public Interaction PlayerInteraction;
    [NonSerialized]
    public Interaction AmberInteraction;


    private void Awake()
    {

        Interaction[] interactions = GetComponents<Interaction>();

        Debug.Assert(interactions.Length > 0, name + " has no interactions tied to it");

        bool hasPlayer = false, hasAmber = false;

        foreach (Interaction i in interactions)
        {
            if (i.isPlayer)
            {
                Debug.Assert(!hasPlayer, name + " has more than one interaction for player");

                PlayerInteraction = i;
                hasPlayer = true;
            }
            else
            {
                Debug.Assert(!hasAmber, name + " has more than one interaction for amber");

                AmberInteraction = i;
                hasAmber = true;
            }
        }
    }

}