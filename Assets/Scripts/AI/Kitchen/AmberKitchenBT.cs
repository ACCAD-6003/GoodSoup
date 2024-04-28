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
        private Node TendToSink() {
            return new WrapperNode(new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.MoveObjects[_interactions.SinkClean.PlayerInteraction.interactionId], true), new List<Node>() {
                    new PutInProgress(true, _interactions.SinkClean.AmberInteraction),
                    new WaitFor(0.5f),
                    new MoveToTile(_interactions.Grid, _interactions.SinkClean.AssociatedTile, new Vector3(-1,0,0)),
                    new WaitFor(0.5f),
                    new PutInProgress(false, _interactions.SinkClean.AmberInteraction),
                    new PerformAmberInteraction(_interactions.SinkClean.AmberInteraction)
            });
        }
        private Node EndingSequence() {
            return new Sequence(new List<Node>() {
                new EvaluateFood(),

                new WaitFor(4f),
                new SwitchAmberMount(navigation),

                new ChangeStoryData<bool>(StoryDatastore.Instance.WearingChefHat, false),
                new WaitFor(0.5f),
                new MoveToTile(_interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.HALLWAY], new Vector3(0,0,-1)),
                new WaitFor(0.5f),

                new ChangeStoryData<GamePhase>(StoryDatastore.Instance.CurrentGamePhase, GamePhase.SLEEP_TIME),
                new AmberMoveToRoom(MainSceneLoading.AmberRoom.HALLWAY)
            });
        }
        private Node OpenPantry() {
            return new Sequence(new List<Node>() {
                new MoveToTile(_interactions.Grid, _interactions.PantryDoor.AssociatedTile),
                new WaitFor(1f),
                new PerformAmberInteraction(_interactions.PantryDoor.AmberInteraction),
                new WaitFor(0.5f),
            });
        }
        private Node ClosePantry()
        {
            return new Sequence(new List<Node>() {
                new PerformAmberInteraction(_interactions.PantryDoor.AmberInteraction),
                new WaitFor(0.5f),
            });
        }
        private Node GoToOvenOpenAndClose() {
            return new Sequence(new List<Node>() {
                new MoveToTile(_interactions.Grid, _interactions.StoveOpen.AssociatedTile),
                new PerformAmberInteraction(_interactions.StoveOpen.AmberInteraction),
                new WaitFor(0.5f),
                new PerformAmberInteraction(_interactions.StoveOpen.AmberInteraction),
                new WaitFor(0.5f)
            });
        }

        private Node GoToFridgeOpenAndClose() {

            return new Sequence(new List<Node>() {
                new WaitFor(0.5f),
                new MoveToTile(_interactions.Grid, _interactions.FridgeOpen.AssociatedTile),
                NoticeParanoiaAndCloseDoor()
            });
        }
        private Node NoticeParanoiaAndCloseDoor() {
            if (StoryDatastore.Instance.MoveObjects[77].Value && !noticedLetters) {
                noticedLetters = true;
                UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.PARANOID, 3f);
                StoryDatastore.Instance.Paranoia.Value += 3f;
            }
            return new Sequence(new List<Node>() {
                new WaitFor(1f),
                new PerformAmberInteraction(_interactions.FridgeOpen.AmberInteraction),
                new WaitFor(1f),
                new PerformAmberInteraction(_interactions.FridgeOpen.AmberInteraction),
                new WaitFor(0.5f),
            });
        }

        // if good soup then restart the kitchen sequence
        // set salt, pepper stats to zero
        // set food quality back to zero
        // delete pot hierarchy of objects (might not be that simple)
        // also factor into account smoke alarm going off
        private Node GoToChair() {
            return new Sequence(new List<Node>() {
                new WaitFor(0.5f),
                new MoveToTile(_interactions.Grid, _interactions.ChairTile, Vector3.back),
                new WaitFor(0.3f),
                new SwitchAmberMount(_interactions.chairMount),
            });
        }
        private Node SitDownSequence() {
            return new Sequence(new List<Node>() {
                GoToChair(),
                new PutAlarmClockAndChairNotInProgress(_interactions),
                new RunFirstSequenceWhereSkipConditionIsTrue(new Dictionary<ISkipCondition, Sequence>()
                    {
                        // Wait and respond to chair falling
                        {   new MovePerformed(_interactions.ChairPull.PlayerInteraction.interactionId), 
                            new Sequence(new List<Node>() {
                                new SwitchAmberMount(_interactions.amberFall),
                                new CameraShakeNode(),
                                new DebugNode(100),
                                new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE, 3f),
                                new ImpactStoryData(StoryDatastore.Instance.Annoyance, 1f),
                                new WaitFor(1.5f),
                                new SwitchAmberMount(navigation),
                                new PerformAmberInteraction(_interactions.ChairPull.AmberInteraction),
                                new PutInProgress(true, _interactions.ChairPull.PlayerInteraction),
                                new PutInProgress(true, _interactions.AlarmTable.PlayerInteraction),
                                new WaitFor(0.25f),
                        }) 
                    },
                    // Wait and respond to alarm
                        {  new MovePerformed(_interactions.AlarmTable.PlayerInteraction.interactionId), 
                            new Sequence(new List<Node>() {
                                new DisplayUIIcon(UIElements.BubbleIcon.OK_IM_COMING, 3f),
                                new DebugNode(101),
                                new PutInProgress(true, _interactions.ChairPull.PlayerInteraction),
                                new SwitchAmberMount(navigation),
                                new MoveToTile(_interactions.Grid, _interactions.AlarmTable.AssociatedTile),
                                new WaitFor(0.5f),
                                new PerformAmberInteraction(_interactions.AlarmTable.AmberInteraction),
                                new PutInProgress(true, _interactions.AlarmTable.PlayerInteraction)
                            })
                        },
                    // Wait and respond to GOOD SOUP!
                    { new GoodSouped(), new Sequence( new List<Node>() {
                            new WaitFor(1f),
                            new DisplayUIIcon(UI.UIElements.BubbleIcon.HAPPY, 5f),
                            new ImpactStoryData(StoryDatastore.Instance.Happiness, 10f),
                            new PutInProgress(true, _interactions.ChairPull.PlayerInteraction),
                            new PutInProgress(true, _interactions.AlarmTable.PlayerInteraction),
                            new SwitchAmberMount(navigation),
                            new WaitFor(0.25f),
                    })    
                    },
                })
            });
        }
        public class ResetKitchen : Node {
            bool _evaluated = false;
            KitchenInteractions _interactions;
            public ResetKitchen(KitchenInteractions interactions) {
                _interactions = interactions;
            }
            public override NodeState Evaluate()
            {
                if (!_evaluated) {
                    _evaluated = true;
                    StoryDatastore.Instance.ActivelyCooking.Value = false;
                    StoryDatastore.Instance.FoodQuality.Value = 0f;
                    StoryDatastore.Instance.HeatSetting.Value = HeatSetting.LOW_TEMP;
                    _interactions.MoveTrayToBurner.gameObject.SetActive(false);
                    _interactions.MoveTrayToDinnerTable.gameObject.SetActive(false);
                    _interactions.MoveTrayToTable.gameObject.SetActive(false);
                }
                state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
        }
        public class PutAlarmClockAndChairNotInProgress : Node
        {
            bool _evaluated = false;
            KitchenInteractions _interactions;
            public PutAlarmClockAndChairNotInProgress(KitchenInteractions _interactions) {
                this._interactions = _interactions;
            }
            public override NodeState Evaluate()
            {
                if (!_evaluated) {
                    _evaluated = true;
                    _interactions.ChairPull.PlayerInteraction.EndAction();
                    _interactions.AlarmTable.PlayerInteraction.EndAction();
                }
                state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
        }
        public class RunFirstSequenceWhereSkipConditionIsTrue : Node {
            private Dictionary<ISkipCondition, Sequence> dictionary;
            Sequence sequenceWeAreRunning = null;
            public RunFirstSequenceWhereSkipConditionIsTrue(Dictionary<ISkipCondition, Sequence> dictionary) {
                this.dictionary = dictionary;
            }
            public override NodeState Evaluate()
            {
                if (sequenceWeAreRunning == null)
                {
                    foreach (var sequence in dictionary) {
                        if (sequence.Key.ShouldSkip()) {
                            sequenceWeAreRunning = sequence.Value;
                            break;
                        }
                    }
                    state = NodeState.RUNNING;
                    return NodeState.RUNNING;
                }
                else {
                    state = sequenceWeAreRunning.Evaluate();
                    return state;
                }
            }

        }
        public class EvaluateFood : Node
        {
            bool _evaluated = false;
            public override NodeState Evaluate()
            {
                if (!_evaluated) {
                    _evaluated = true;
                    if (StoryDatastore.Instance.FoodQuality.Value < 0f)
                    {
                        UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.DAMN_THIS_FOOD_BLOWS, 3f);
                        StoryDatastore.Instance.Annoyance.Value += 3f;
                    }
                    else if (StoryDatastore.Instance.FoodQuality.Value > 0f) 
                    {
                        UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.POG_CHEF, 3f);
                    }
                }
                state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }
        }
        public class ChangeStoryData<T> : Node {
            T newValue;
            bool _changed = false;
            StoryData<T> _storyData;
            public ChangeStoryData (StoryData<T> data, T newValue) {
                _storyData = data;
                this.newValue = newValue;
            }
            public override NodeState Evaluate()
            {
                if (!_changed) {
                    _changed = true;
                    _storyData.Value = newValue;
                }
                state = NodeState.SUCCESS;
                return state;
            }

        }
        class AmberNoticeRecipe : Node {
            bool _increased = false;
            bool _coolingDown = false;
            float timePassed = 0f;
            AmberKitchenBT _amberKitchenBT;
            InteractableObject _floatingRecipe;
            public AmberNoticeRecipe(AmberKitchenBT kitchenBT, InteractableObject floatingRecipe)
            { 
                _amberKitchenBT = kitchenBT;
                _floatingRecipe = floatingRecipe;
            }
            public override NodeState Evaluate()
            {
                if (!StoryDatastore.Instance.GoodSoupPuzzleSolved.Value) {
                    if (!_coolingDown && _amberKitchenBT.GetObjectInteraction().IsInAmberSightlines(_floatingRecipe) && _floatingRecipe.gameObject.activeInHierarchy)
                    {
                        if (!_increased)
                        {
                            _increased = true;
                            StoryDatastore.Instance.Paranoia.Value += 3f;
                        }
                        UIManager.Instance.DisplaySimpleBubbleForSeconds(UIElements.BubbleIcon.PARANOID, 2f);
                        _coolingDown = true;
                    }
                    else
                    {
                        timePassed += Time.deltaTime;
                        if (timePassed > 7f)
                        {
                            timePassed = 0f;
                            _coolingDown = false;
                        }
                    }
                }
                state = NodeState.SUCCESS;
                return state;
            }
        }
        class CameraShakeNode : Node {
            bool _performed;
            CameraShake _shake;
            public CameraShakeNode() {
                _shake = FindObjectOfType<CameraShake>();
                _performed = false;
            }
            public override NodeState Evaluate() {
                if (!_performed) {
                    _performed = true;
                    _shake.ShakeCamera();
                }
                state = NodeState.SUCCESS;
                return state;
            }
        }
        class PutInProgress : Node {
            bool _inProgress, _performed = false;
            Interaction _interaction;
            public PutInProgress(bool inProgress, Interaction interaction) { 
                _inProgress = inProgress;
                _interaction = interaction;
            }
            public override NodeState Evaluate()
            {
                if (!_performed) { 
                    _performed  = true;
                    if (_inProgress)
                    {
                        _interaction.PutInProgress();
                    }
                    else {
                        _interaction.EndAction();
                    }
                }
                state = NodeState.SUCCESS;
                return state;
            }
        }
        class MovePerformed : ISkipCondition
        {
            int ID;
            public MovePerformed(int ID) { 
                this.ID = ID;
            }
            public bool ShouldSkip()
            {
                return StoryDatastore.Instance.MoveObjects[ID].Value;
            }
        }
        class GoodSouped : ISkipCondition
        {
            public bool ShouldSkip()
            {
                return StoryDatastore.Instance.GoodSoupPuzzleSolved.Value;
            }
        }
    }**/

    }
    public class ImpactStoryData : Node
    {
        float impact;
        StoryData<float> storyData;
        bool _performed = false;
        public ImpactStoryData(StoryData<float> data, float impact)
        {
            this.impact = impact;
            storyData = data;
        }
        public override NodeState Evaluate()
        {
            if (!_performed)
            {
                storyData.Value += impact;
                _performed = true;
            }
            state = NodeState.SUCCESS;
            return state;
        }

    }
}