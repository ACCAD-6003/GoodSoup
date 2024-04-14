using Assets.Scripts.AI;
using Assets.Scripts.Objects.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class BathroomInteractions : MonoBehaviour
    {
        public grid_manager Grid;
        public tile showerTile, LaundryBasketTile, sinkTile;
        public AmberUseShower Shower;
        public FlushToilet FlushToilet;
        public InteractableObject Towel, ShowerCurtain;
        public AmberMount shower;
    }
}
