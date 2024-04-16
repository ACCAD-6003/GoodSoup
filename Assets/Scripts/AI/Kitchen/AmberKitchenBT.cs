using Assets.Scripts.AI.Kitchen;
using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI
{
    public class AmberKitchenBT : BehaviorTree.Tree
    {
        [SerializeField]
        KitchenInteractions _interactions;
        protected override Node SetupTree()
        {
            return new Sequence(new List<Node>() {
                new WaitFor(0.5f),
                new MoveToTile(_interactions.Grid, _interactions.ChairTile, Vector3.back),
                new SwitchAmberMount(_interactions.chairMount)
            });
        }
    }
}
