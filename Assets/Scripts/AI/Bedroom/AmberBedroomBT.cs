using Assets.Scripts.AI.Bedroom;
using Assets.Scripts.AI.GeneralNodes;
using Assets.Scripts.AI.Kitchen;
using Assets.Scripts.Objects;
using Assets.Scripts.UI;
using BehaviorTree;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ComputerHUD;


namespace Assets.Scripts.AI
{
    public class AmberBedroomBT : BehaviorTree.Tree
    {
        [SerializeField] BedroomInteractions interactions;
        AmberMount navigation;
        StoryDatastore storyData;
        Doors doors;
        private void AdaptToSceneChanges()
        {
            doors = FindObjectOfType<Doors>();
            FindObjectOfType<BedroomInteractions>();
        }

        protected override Node SetupTree()
        {
            AdaptToSceneChanges();
            navigation = GameObject.FindGameObjectWithTag("Player").GetComponent<AmberMount>();
            storyData = StoryDatastore.Instance;

            Node routine = 
            new Sequence(new List<Node>()
            {
                new WrapperNode(new SkipIfStoryDatastoreState<GamePhase>(StoryDatastore.Instance.CurrentGamePhase, GamePhase.TUTORIAL_BEDROOM, true), new List<Node>()
                {
                    // change to evaluate if everything has been done one by one by wrapping these in classespho
                    new DisplayUIIcon(UI.UIElements.BubbleIcon.SLEEPING),
                    new WaitForPlayerInteractionCompleted(interactions.AlarmClock),
                    new StopFarCryEnding(interactions.switcher),
                    new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                    new WaitFor(1f),
                    new SwitchAmberMount(interactions.SittingInBed),
                    new WaitFor(1f),
                    new DisplayUIIcon(UI.UIElements.BubbleIcon.PHONE),
                    new WaitForBookHit(storyData.AnyBookDropped),
                    new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                    new WaitFor(1f),
                    new SwitchAmberMount(navigation),
                    new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.BATHROOM]),
                    new AmberMoveToRoom(MainSceneLoading.AmberRoom.BATHROOM)
                }),
                new StopFarCryEnding(interactions.switcher),
                new WrapperNode(new SkipIfStoryDatastoreState<EmailState>(StoryDatastore.Instance.EmailState, EmailState.NOTHING_CHANGED, false), new List<Node>() {
                    // glitch here, if player is interacting with email then amber could possibly get stuck
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, interactions.Laptop.AssociatedTile),
                    new SwitchAmberMount(interactions.SittingAtDesk),
                    new WaitFor(1f),
                    new PerformAmberInteraction(interactions.Laptop.AmberInteraction)
                }),
                new WaitFor(2f),
                new SelectEnding(),
            });
            return routine;
        }
    }
}