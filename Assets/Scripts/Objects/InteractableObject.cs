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

        Debug.Assert(interactions.Length < 2);

        foreach (Interaction i in interactions)
        {
            if (i.isPlayer)
            {
                PlayerInteraction = i;
            }
            else
            {
                AmberInteraction = i;
            }
        }
    }

}