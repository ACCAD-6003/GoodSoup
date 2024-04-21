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
                    // Wait for alarm clock
                    new WrapperNode(new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.BooksBlown, true), new List<Node>() {
                        new Sequence(new List<Node>() {
                            new WaitForPlayerInteractionCompleted(interactions.AlarmClock),
                            new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                        }),
                        new StopFarCryEnding(interactions.switcher),

                        new WaitFor(1f),

                        new SetGameObjectActive(interactions.phoneOnTable, false),

                        new SwitchAmberMount(interactions.SittingInBed),
                        new WaitFor(1f),
                        new DisplayUIIcon(UI.UIElements.BubbleIcon.PHONE),
                    }),
                    new SetGameObjectActive(interactions.phoneOnTable, false),
                    // Wait for book hit
                    new Selector(new List<Node>() {
                        new Sequence(new List<Node>() {
                            new WaitForBookHit(storyData.AnyBookDropped),
                            new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                        }),
                        new Sequence(new List<Node>() {
                            new WaitForStoryDataChange(new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.CurtainsOpen, true)),
                            new DisplayUIIcon(UI.UIElements.BubbleIcon.HAPPY_SUNSHINE),
                        })
                    }),

                    new WaitFor(1f),
                    new SwitchAmberMount(navigation),

                    // DEBUG
                    //new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.KITCHEN]),
                    //new AmberMoveToRoom(MainSceneLoading.AmberRoom.KITCHEN),
                    

                    new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.BATHROOM]),
                    new AmberMoveToRoom(MainSceneLoading.AmberRoom.BATHROOM)
                }),

                new SetGameObjectActive(interactions.phoneOnTable, false),
                new StopFarCryEnding(interactions.switcher),


                // make amber open curtains if she needs to
                new WrapperNode(new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.CurtainsOpen, true), new List<Node> { 
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, interactions.Curtains.AssociatedTile),
                    new WaitFor(0.5f),
                    new PerformAmberInteraction(interactions.Curtains.AmberInteraction),
                }, true),

                new WaitFor(2f),

                // ONLY skips if it is true when this tree starts to evalute that Amber has already dressed. Doesn't check again.
                new WrapperNode(new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.AmberDressed, true), new List<Node>() {
                    new MoveToTile(interactions.Grid, interactions.Dresser.AssociatedTile),
                    new WaitFor(1f),
                    new PerformAmberInteraction(interactions.Dresser.AmberInteraction),
                    new WaitFor(1f),
                    new MoveToTile(interactions.Grid, interactions.MirrorTile),
                    new AmberReactionToStoryData<ClothingOption>(new Dictionary<ClothingOption, IAmberReaction>() {
                        { ClothingOption.Dirty, new DirtyClothingReaction() },
                        { ClothingOption.Blue, new HappyClothingReaction() },
                        { ClothingOption.Orange, new HappyClothingReaction() },
                        { ClothingOption.Green, new HappyClothingReaction() },
                    }, StoryDatastore.Instance.AmberWornClothing),
                    new WaitFor(2f),
                }, true),

                new WrapperNode(new SkipIfStoryDatastoreState<GamePhase>(StoryDatastore.Instance.CurrentGamePhase, GamePhase.BEFORE_AMBER_LEAVES, true), new List<Node>() {
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, interactions.Backpack.AssociatedTile),
                    new PerformAmberInteraction(interactions.Backpack.AmberInteraction),
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.GONE], new Vector3(0,0,-1)),
                    new WaitFor(0.25f),
                    new AmberMoveToRoom(MainSceneLoading.AmberRoom.GONE),
                    new WaitFor(5f),
                }),

                new WrapperNode(new SkipIfStoryDatastoreState<EmailState>(StoryDatastore.Instance.EmailState, EmailState.NOTHING_CHANGED, false), new List<Node>() {
                    // glitch here, if player is interacting with email then amber could possibly get stuck
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, interactions.Laptop.AssociatedTile),
                    new SwitchAmberMount(interactions.SittingAtDesk),
                    new WaitFor(1f),
                    new PerformAmberInteraction(interactions.Laptop.AmberInteraction),
                    new WaitFor(2f),
                    new SwitchAmberMount(navigation),
                }, true),

                new WrapperNode(new SkipIfStoryDatastoreState<GamePhase>(StoryDatastore.Instance.CurrentGamePhase, GamePhase.SLEEP_TIME), new List<Node>() {
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, interactions.Backpack.AssociatedTile),
                    new PerformAmberInteraction(interactions.Backpack.AmberInteraction),
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.KITCHEN]),
                    new WaitFor(0.25f),
                    new AmberMoveToRoom(MainSceneLoading.AmberRoom.KITCHEN),
                    new WaitFor(1f),
                }),

                new MoveToTile(interactions.Grid, interactions.BedTile),
                new WaitFor(0.5f),
                new SwitchAmberMount(interactions.LayingInBed),
                new WaitFor(2f),

                new SelectEnding(),
            });

            return routine;
        }
    }
    class HappyClothingReaction : IAmberReaction
    {
        public override void PerformReaction()
        {
            UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UI.UIElements.BubbleIcon.SICK_OUTFIT);
            StoryDatastore.Instance.Happiness.Value += 2f;
        }
    }
    class DirtyClothingReaction : IAmberReaction
    {
        public override void PerformReaction()
        {
            UIManager.Instance.DisplaySimpleBubbleTilInterrupted(UI.UIElements.BubbleIcon.DIRTY_OUTFIT);
            StoryDatastore.Instance.Annoyance.Value += 1f;
        }
    }
}
