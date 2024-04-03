using Assets.Scripts.AI.GeneralNodes;
using Assets.Scripts.AI.Kitchen;
using Assets.Scripts.Objects;
using Assets.Scripts.UI;
using BehaviorTree;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


namespace Assets.Scripts.AI
{
    public class AmberBathroomBT : BehaviorTree.Tree
    {
        [SerializeField] BathroomInteractions interactions;
        AmberMount navigation;
        StoryDatastore storyData;
        Doors doors;
        private void Awake()
        {
            AdaptToSceneChanges();
        }

        private void AdaptToSceneChanges()
        {
            doors = FindObjectOfType<Doors>();
            FindObjectOfType<BedroomInteractions>();
        }

        protected override Node SetupTree()
        {
            navigation = GameObject.FindGameObjectWithTag("Player").GetComponent<AmberMount>();
            storyData = StoryDatastore.Instance;

            Node routine = new Sequence(new List<Node>() {
                        new WaitFor(0.25f),
                        new MoveToTile(interactions.Grid, interactions.showerTile),
                        new AmberShower(interactions.Shower),
                        new WaitFor(0.5f),
                        new MoveToTile(interactions.Grid, interactions.LaundryBasketTile),
                        new EvaluateItemsPickedUp(new List<int>() { 0,1,2 }, UIElements.BubbleIcon.HAPPY_LAUNDRY, UIElements.BubbleIcon.SAD_LAUNDRY),
                        new WaitFor(0.5f),
                        new MoveToTile(interactions.Grid, interactions.sinkTile),
                        new DebugNode(1),
                        new WrapperNode(
                            new SkipIfStoryDatastoreState<MirrorState>(StoryDatastore.Instance.MirrorState, MirrorState.DRAWN_ON, true),
                            new List<Node>() {
                                new DebugNode(2),
                                new WaitFor(0.25f),
                                new DebugNode(3),
                                new ModifyStat(StoryDatastore.Instance.Paranoia, 0.1f),
                                new DebugNode(4),
                                new ShowBubble(UIElements.BubbleIcon.PARANOID),
                                new DebugNode(5),
                                new WaitFor(1f),
                                new DebugNode(6),
                            }
                        ),
                        new DebugNode(7),
                        new WaitFor(0.5f),
                        new EvaluateItemsPickedUp(new List<int>() { 1000 }, UIElements.BubbleIcon.OH_WHERE_IS_MY_HAIRBRUSH, UIElements.BubbleIcon.BRUSH),
                        new WrapperNode(
                            new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.ResultOfEvaluation, true),
                            new List<Node>() { 
                                new WaitFor(3f),
                                new AmberHairBrush()
                            }
                        ),
                        new WaitFor(0.5f),
                        new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.BEDROOM]),
                        new AmberMoveToRoom(MainSceneLoading.AmberRoom.BEDROOM)
                    });

            return routine;
        }
    }
}
