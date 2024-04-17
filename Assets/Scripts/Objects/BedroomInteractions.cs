using Assets.Scripts.AI;
using Assets.Scripts.AI.Bedroom;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BedroomInteractions : MonoBehaviour
    {
        public InteractableObject AlarmClock, Laptop, Curtains, Dresser, Backpack;
        public List<InteractableObject> Books;
        public AmberMount LayingInBed, SittingInBed, SittingAtDesk;
        public FarCrySwitcher switcher;
        public tile MirrorTile, BedTile;
        public grid_manager Grid;
        public GameObject phoneOnTable;
    }
}
