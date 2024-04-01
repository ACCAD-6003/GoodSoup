using Assets.Scripts.AI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BedroomInteractions : MonoBehaviour
    {
        public InteractableObject AlarmClock, Laptop, Curtains;
        public List<InteractableObject> Books;
        public AmberMount LayingInBed, SittingInBed;
        public grid_manager Grid;
        public tile BathroomTile;
    }
}
