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
    public class AmberBedroomBT : BehaviorTree.Tree
    {
        [SerializeField] BedroomInteractions interactions;
        AmberMount navigation;
        StoryDatastore storyData;
        Doors doors;
        private void Awake()
        {
            SceneManager.activeSceneChanged += AdaptToSceneChanges;
        }

        private void AdaptToSceneChanges(Scene arg0, Scene arg1)
        {
            doors = FindObjectOfType<Doors>();
            FindObjectOfType<BedroomInteractions>();
        }

        protected override Node SetupTree()
        {
            navigation = GameObject.FindGameObjectWithTag("Player").GetComponent<AmberMount>();
            storyData = StoryDatastore.Instance;

            Node routine = new WrapperNode(new SkipIfStoryDatastoreState<GamePhase>(StoryDatastore.Instance.CurrentGamePhase, GamePhase.TUTORIAL_BEDROOM, true), new List<Node>()
            {
                // change to evaluate if everything has been done one by one by wrapping these in classespho
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
                new AmberMoveToRoom(MainSceneLoading.AmberRoom.BATHROOM)
            });
            return routine;
        }
    }
}
