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
                new DisplayUIIcon(UI.UIElements.BubbleIcon.SLEEPING),
                new WaitForPlayerInteractionCompleted(alarmClockClick),
                new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                new WaitFor(1f),
                new SwitchAmberMount(sitInBed),
                new WaitFor(1f),
                new DisplayUIIcon(UI.UIElements.BubbleIcon.PHONE),
                new WaitForBookHit(storyData.AnyBookDropped),
                new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                new WaitFor(1f),
                new SwitchAmberMount(navigation),
                new MoveToTile(grid, _bathroomDoor),
                new AmberMoveToRoom("Bathroom", "Bedroom")
            });
            return routine;
        }
    }
}
