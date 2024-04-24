using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenInteractions : MonoBehaviour
{
    [SerializeField]
    public grid_manager Grid;
    [SerializeField] 
    public tile DebugTile, ChairTile, Crockpot;
    [SerializeField]
    public InteractableObject PantryDoor, AlarmShelf, AlarmTable, ChairPull, SinkClean, FridgeOpen, StoveOpen, FloatingRecipe;
    [SerializeField]
    public GameObject chairInScene;
    [SerializeField]
    public AmberMount chairMount, amberFall;
}
