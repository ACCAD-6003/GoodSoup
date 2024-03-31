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
        [SerializeField]
        StoryDatastore storyDatastore;
        protected override Node SetupTree()
        {
            // initialize to real first node
            Node turnDownHeat = new Sequence(new List<Node>(){
                new CheckIfICareAboutBurner(storyDatastore), new MoveToTile(grid, interactableObject.AssociatedTile), new PerformInteraction(interactableObject, grid)
            });
            Node standByFridge = new Sequence(new List<Node>() {
                new MoveToTile(grid, _fridgeTile)
            });
            Node selector = new Selector(new List<Node>() { standByFridge, turnDownHeat });
            return selector;
        }
    }
}
