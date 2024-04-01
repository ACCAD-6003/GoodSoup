using Assets.Scripts.AI.Kitchen;
using Assets.Scripts.UI;
using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI
{
    public class AmberBedroomBT : BehaviorTree.Tree
    {
        [SerializeField] BedroomInteractions interactions;
        AmberMount navigation;
        StoryDatastore storyData;
        protected override Node SetupTree()
        {
            navigation = GameObject.FindGameObjectWithTag("Player").GetComponent<AmberMount>();
            storyData = StoryDatastore.Instance;
            Node routine = new Sequence(new List<Node>()
            {
                new DisplayUIIcon(UI.UIElements.BubbleIcon.SLEEPING),
                new WaitForPlayerInteractionCompleted(interactions.AlarmClock),
                new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                new WaitFor(1f),
                new SwitchAmberMount(interactions.SittingInBed),
                new WaitFor(1f),
                new DisplayUIIcon(UI.UIElements.BubbleIcon.PHONE),
                new WaitForBookHit(storyData.AnyBookDropped),
                new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                new WaitFor(1f),
                new SwitchAmberMount(navigation),
                new MoveToTile(interactions.Grid, interactions.BathroomTile),
                new AmberMoveToRoom("Bathroom", "Bedroom")
            });
            return routine;
        }
    }
}
