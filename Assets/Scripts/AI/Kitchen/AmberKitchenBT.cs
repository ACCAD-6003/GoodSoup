using Assets.Scripts.AI.GeneralNodes;
using Assets.Scripts.AI.Kitchen;
using Assets.Scripts.Objects;
using Assets.Scripts.UI;
using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI
{
    public class AmberKitchenBT : BehaviorTree.Tree
    {
        /**bool noticedLetters = false;
        [SerializeField]
        KitchenInteractions _interactions;
        AmberMount navigation;
        Doors doors;
        ObjectInteraction objInteraction;
        private void AdaptToSceneChanges()
        {
            doors = FindObjectOfType<Doors>();
        }
        protected ObjectInteraction GetObjectInteraction() {
            return FindObjectOfType<ObjectInteraction>();
        }
        /**protected override Node SetupTree()
        {

            AdaptToSceneChanges();
            navigation = GameObject.FindGameObjectWithTag("Player").GetComponent<AmberMount>();
            return new Sequence(new List<Node>() {
                new ChangeStoryData<bool>(StoryDatastore.Instance.WearingChefHat, true),
                new AmberNoticeRecipe(this, _interactions.FloatingRecipe),
                new WaitFor(0.5f),

                new SwitchAmberMount(_interactions.chairMount),
                new WaitFor(0.5f),
                new SwitchAmberMount(navigation),

                new WrapperNode(new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.GoodSoupPuzzleSolved, true), new List<Node>() {
                    CreateKitchenSeq(false)
                }),
                CreateKitchenSeq(true)
            });

        }
        private Node CreateKitchenSeq(bool isGoodSoup) {
            _interactions.ChairPull.PlayerInteraction.PutInProgress();
            _interactions.AlarmTable.PlayerInteraction.PutInProgress();
            if (isGoodSoup) {
                return new Sequence(new List<Node>() {
                    new DisplayUIIcon(UI.UIElements.BubbleIcon.HAPPY, 5f),
                    new ImpactStoryData(StoryDatastore.Instance.Happiness, 10f),
                    new PutInProgress(true, _interactions.ChairPull.PlayerInteraction),
                    new PutInProgress(true, _interactions.AlarmTable.PlayerInteraction),

                    new ResetKitchen(_interactions),
                    new WaitFor(2f),
                    new SwitchAmberMount(navigation),

                    OpenPantry(),

                    new PerformAmberInteraction(_interactions.MovePotFromCabinetToTable.AmberInteraction),
                    new WaitFor(0.5f),

                    ClosePantry(),

                    SitDownSequence(),

                    new WaitFor(0.5f),
                    new MoveToTile(_interactions.Grid, _interactions.StoveOpen.AssociatedTile),
                    new PerformAmberInteraction(_interactions.MovePotFromTableToBurner.AmberInteraction),
                    new ChangeStoryData<bool>(StoryDatastore.Instance.ActivelyCooking, true),

                    TendToSink(),

                    GoToFridgeOpenAndClose(),

                    SitDownSequence(),

                    GoToFridgeOpenAndClose(),

                    GoToOvenOpenAndClose(),

                    new ChangeStoryData<bool>(StoryDatastore.Instance.ActivelyCooking, false),


                    GoToChair(),

                    new PerformAmberInteraction(_interactions.MovePotFromBurnerToDinnerTable.AmberInteraction),

                    EndingSequence(),

                    new WaitFor(5f)
                });
            }
            return new Sequence(new List<Node>() {
                OpenPantry(),

                new PerformAmberInteraction(_interactions.MoveTrayToTable.AmberInteraction),
                new WaitFor(0.5f),

                ClosePantry(),

                // ONLY A PART OF KITCHEN SEQ
                new MoveToTile(_interactions.Grid, _interactions.AlarmShelf.AssociatedTile),
                new WaitFor(0.5f),
                new PerformAmberInteraction(_interactions.AlarmShelf.AmberInteraction),
                new WaitFor(0.5f),
                // ONLY A PART OF KITCHEN SEQ

                SitDownSequence(),

                GoToOvenOpenAndClose(),

                new PerformAmberInteraction(_interactions.MoveTrayToBurner.AmberInteraction),
                new ChangeStoryData<bool>(StoryDatastore.Instance.ActivelyCooking, true),

                TendToSink(),

                SitDownSequence(),

                GoToFridgeOpenAndClose(),

                GoToOvenOpenAndClose(),

                new ChangeStoryData<bool>(StoryDatastore.Instance.ActivelyCooking, false),


                GoToChair(),

                new PerformAmberInteraction(_interactions.MoveTrayToDinnerTable.AmberInteraction),

                EndingSequence(),

                new WaitFor(5f)
            });
        }

    }**/

    }
}