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
    public class HallwayBT : BehaviorTree.Tree
    {
        [SerializeField] HallwayInteractions interactions;
        AmberMount navigation;
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

            Node routine = new Sequence(new List<Node>() {
                new DebugNode(StoryDatastore.Instance.HallwayVisits.Value),
                new WrapperNode(new SkipIfStoryDatastoreState<int>(StoryDatastore.Instance.HallwayVisits, 1, true), new List<Node>() {
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, interactions.Door.AssociatedTile, Vector3.left),
                    new WaitFor(0.5f),
                    new PerformAmberInteraction(interactions.Door.AmberInteraction),
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.GONE], Vector3.left),
                    new WaitFor(0.5f),
                    new PerformAmberInteraction(interactions.Door.AmberInteraction),
                    new WaitFor(0.5f),
                    new AmberMoveToRoom(MainSceneLoading.AmberRoom.GONE),
                    new WaitFor(0.5f),
                }),
                new WrapperNode(new SkipIfStoryDatastoreState<int>(StoryDatastore.Instance.HallwayVisits, 2, true), new List<Node>() {
                    new WaitFor(0.5f),
                    new WrapperNode(new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.AmberPickedUpKey, true), new List<Node>() { 
                        new WaitFor(0.5f),
                        new PlaySound(interactions.src, interactions.knock),
                        new WaitFor(2f),
                        new PlaySound(interactions.src, interactions.knock),
                        new WaitFor(2f),
                        new PlaySound(interactions.src, interactions.knock),
                        new WaitFor(2f),
                        new GoStraightToEnding(Ending.LOCKED_OUT)
                    }),
                    new WaitFor(0.5f),
                    new PerformAmberInteraction(interactions.Door.AmberInteraction),
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, interactions.Door.AssociatedTile, Vector3.left),
                    new WaitFor(0.5f),
                    new PerformAmberInteraction(interactions.Door.AmberInteraction),
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.BEDROOM]),
                    new WaitFor(0.5f),
                    new AmberMoveToRoom(MainSceneLoading.AmberRoom.BEDROOM)
                }),
                new WrapperNode(new SkipIfStoryDatastoreState<int>(StoryDatastore.Instance.HallwayVisits, 3, true), new List<Node>() {
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.KITCHEN]),
                    new WaitFor(0.5f),
                    new AmberMoveToRoom(MainSceneLoading.AmberRoom.KITCHEN)
                }),
                new WrapperNode(new SkipIfStoryDatastoreState<int>(StoryDatastore.Instance.HallwayVisits, 4, true), new List<Node>() {
                    new WaitFor(0.5f),
                    new MoveToTile(interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.BEDROOM]),
                    new WaitFor(0.5f),
                    new AmberMoveToRoom(MainSceneLoading.AmberRoom.BEDROOM)
                })

            });

            return routine;
        }
    }
}
