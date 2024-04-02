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
        public Shower Shower;
        public FlushToilet FlushToilet;
    }
}
