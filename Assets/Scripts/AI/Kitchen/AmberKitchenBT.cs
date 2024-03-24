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
        protected override Node SetupTree()
        {
            // initialize to real first node
            Node root = new Sequence(new List<Node>(){
                new MoveToTile(grid, interactableObject.AssociatedTile), new PerformInteraction(interactableObject, grid)
            });
            return root;
        }
    }
}
