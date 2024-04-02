using Assets.Scripts.AI.GeneralNodes;
using Assets.Scripts.AI.Kitchen;
using Assets.Scripts.Objects;
using Assets.Scripts.UI;
using BehaviorTree;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
                new WrapperNode(
                    new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.DoneShowering, true),
                    new List<Node>() {
                        new WaitFor(0.25f),
                        new MoveToTile(interactions.Grid, interactions.showerTile),
                        new AmberShower(interactions.Shower)
                    }
                ),
                new WrapperNode(
                    new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.SinkRoutineDone, true),
                    new List<Node>() {
                        new WaitFor(0.5f),
                        new MoveToTile(interactions.Grid, interactions.LaundryBasketTile),
                        new EvaluateItemsPickedUp(new List<int>() { 0,1,2 }, UIElements.BubbleIcon.HAPPY_LAUNDRY, UIElements.BubbleIcon.SAD_LAUNDRY),
                        new WaitFor(0.5f),
                        new MoveToTile(interactions.Grid, interactions.sinkTile),
                        new WaitFor(1f),
                        new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.BEDROOM]),
                        new AmberMoveToRoom(MainSceneLoading.AmberRoom.BEDROOM)
                    }
                )
            });

            return routine;
        }
    }
}
