using Assets.Scripts.AI.Kitchen;
using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI
{
    public class AmberKitchenBT : BehaviorTree.Tree
    {
        [SerializeField]
        grid_manager grid;
        [SerializeField]
        InteractableObject interactableObject;
        [SerializeField]
        tile _fridgeTile;
        protected override Node SetupTree()
        {
            // initialize to real first node
            Node turnDownHeat = new Sequence(new List<Node>(){
                new MoveToTile(grid, interactableObject.AssociatedTile), new PerformInteraction(interactableObject, grid)
            });
            Node standByFridge = new MoveToTile(grid, _fridgeTile);
            Node selector = new Selector(new List<Node>() { turnDownHeat, standByFridge });
            return selector;
        }
    }
}
