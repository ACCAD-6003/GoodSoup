using Assets.Scripts.AI.Kitchen;
using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI
{
    public class AmberBedroomBT : BehaviorTree.Tree
    {
        [SerializeField]
        grid_manager grid;
        [SerializeField]
        InteractableObject alarmClockClick, bookClick;
        [SerializeField]
        AmberMount sitInBed, navigation;
        [SerializeField]
        tile _bathroomDoor;
        [SerializeField]
        StoryDatastore storyData;
        protected override Node SetupTree()
        {
            Node routine = new Sequence(new List<Node>()
            {
                new WaitForPlayerInteractionCompleted(alarmClockClick), 
                new SwitchAmberMount(sitInBed), 
                new WaitForBookHit(storyData.AnyBookDropped),
                new SwitchAmberMount(navigation),
                new MoveToTile(grid, _bathroomDoor),
                //new AmberMoveToRoom("Bathroom"),
            });
            return routine;
        }
    }
}
