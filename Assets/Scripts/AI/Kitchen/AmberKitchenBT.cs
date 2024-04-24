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
        protected override Node SetupTree()
        {
            AdaptToSceneChanges();
            navigation = GameObject.FindGameObjectWithTag("Player").GetComponent<AmberMount>();
            return new Sequence(new List<Node>() {
                new ChangeStoryData<bool>(StoryDatastore.Instance.WearingChefHat, true),
                new AmberNoticeRecipe(this, _interactions.FloatingRecipe),
                new WaitFor(0.5f),
                // DEBUG
                new SwitchAmberMount(_interactions.chairMount),
                new WaitFor(0.5f),
                new SwitchAmberMount(navigation),
                // END DEBUG
                new MoveToTile(_interactions.Grid, _interactions.PantryDoor.AssociatedTile),
                new WaitFor(0.5f),
                new PerformAmberInteraction(_interactions.PantryDoor.AmberInteraction),
                new WaitFor(1f),
                new PerformAmberInteraction(_interactions.PantryDoor.AmberInteraction),
                new WaitFor(0.5f),

                new MoveToTile(_interactions.Grid, _interactions.AlarmShelf.AssociatedTile),
                new WaitFor(0.5f),
                new PerformAmberInteraction(_interactions.AlarmShelf.AmberInteraction),
                new WaitFor(0.5f),

                new MoveToTile(_interactions.Grid, _interactions.ChairTile, Vector3.back),
                new WaitFor(0.3f),
                new SwitchAmberMount(_interactions.chairMount),

                new PutInProgress(false, _interactions.ChairPull.PlayerInteraction),
                new PutInProgress(false, _interactions.AlarmTable.PlayerInteraction),

                SitDownSequence(),

                new WrapperNode(new SkipIfStoryDatastoreState<bool>(StoryDatastore.Instance.MoveObjects[_interactions.SinkClean.PlayerInteraction.interactionId], true), new List<Node>() {
                    new PutInProgress(true, _interactions.SinkClean.AmberInteraction),
                    new WaitFor(0.5f),
                    new MoveToTile(_interactions.Grid, _interactions.SinkClean.AssociatedTile, new Vector3(-1,0,0)),
                    new WaitFor(0.5f),
                    new PutInProgress(false, _interactions.SinkClean.AmberInteraction),
                    new PerformAmberInteraction(_interactions.SinkClean.AmberInteraction)
                }),

                GoToFridgeOpenAndClose(),

                new MoveToTile(_interactions.Grid, _interactions.Crockpot, new Vector3(0,0,1)),
                new WaitFor(3f),

                GoToFridgeOpenAndClose(),

                new MoveToTile(_interactions.Grid, _interactions.ChairTile, Vector3.back),
                new WaitFor(0.3f),
                new SwitchAmberMount(_interactions.chairMount),

                new PutInProgress(false, _interactions.ChairPull.PlayerInteraction),
                new PutInProgress(false, _interactions.AlarmTable.PlayerInteraction),

                SitDownSequence(),

                GoToFridgeOpenAndClose(),

                GoToOvenOpenAndClose(),

                new WaitFor(0.5f),
                new MoveToTile(_interactions.Grid, doors.doors[MainSceneLoading.AmberRoom.HALLWAY], new Vector3(0,0,-1)),
                new WaitFor(0.5f),
                
                new ChangeStoryData<GamePhase>(StoryDatastore.Instance.CurrentGamePhase, GamePhase.SLEEP_TIME),
                new AmberMoveToRoom(MainSceneLoading.AmberRoom.HALLWAY)
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
                new WaitFor(0.5f),
                new PerformAmberInteraction(_interactions.FridgeOpen.AmberInteraction),
                new WaitFor(1f),
                new PerformAmberInteraction(_interactions.FridgeOpen.AmberInteraction),
                new WaitFor(0.5f),
            });
        }

        private Node SitDownSequence() {
            return new Selector(new List<Node>()
                {
                    // Wait and respond to chair falling
                    new Sequence( new List<Node>() {
                        new WaitForStoryDataChange(new MovePerformed(_interactions.ChairPull.PlayerInteraction.interactionId)),
                        new SwitchAmberMount(_interactions.amberFall),
                        new CameraShakeNode(),
                        new DebugNode(100),
                        new WaitFor(1.5f),
                        new PerformAmberInteraction(_interactions.ChairPull.AmberInteraction),
                        new PutInProgress(true, _interactions.ChairPull.PlayerInteraction),
                        new DebugNode(666),
                        new DisplayUIIcon(UI.UIElements.BubbleIcon.ANNOYANCE),
                        new ImpactStoryData(StoryDatastore.Instance.Annoyance, 1f),
                        new SwitchAmberMount(navigation),
                    }),
                    // Wait and respond to alarm
                    new Sequence( new List<Node>() {
                        new WaitForStoryDataChange(new MovePerformed(_interactions.AlarmTable.PlayerInteraction.interactionId)),
                        new DebugNode(101),
                        new SwitchAmberMount(navigation),
                        new MoveToTile(_interactions.Grid, _interactions.AlarmTable.AssociatedTile),
                        new WaitFor(0.5f),
                        new PerformAmberInteraction(_interactions.AlarmTable.AmberInteraction),
                        new PutInProgress(true, _interactions.AlarmTable.PlayerInteraction)
                    }),
                    // Wait and respond to GOOD SOUP!
                    new Sequence( new List<Node>() {
                        new WaitForStoryDataChange(new GoodSouped()),
                        new DebugNode(102),
                        new DisplayUIIcon(UI.UIElements.BubbleIcon.HAPPY),
                        new ImpactStoryData(StoryDatastore.Instance.Happiness, 10f),
                        new SwitchAmberMount(navigation)
                    }),

                });
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
            public AmberNoticeRecipe(AmberKitchenBT kitchenBT, InteractableObject floatingRecipe) { 
                _amberKitchenBT = kitchenBT;
                _floatingRecipe = floatingRecipe;
            }
            public override NodeState Evaluate()
            {
                if (!StoryDatastore.Instance.GoodSoup.Value) {
                    if (!_coolingDown && _amberKitchenBT.GetObjectInteraction().IsInAmberSightlines(_floatingRecipe))
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
        class ImpactStoryData : Node {
            float impact;
            StoryData<float> storyData;
            bool _performed = false;
            public ImpactStoryData(StoryData<float> data, float impact) {
                this.impact = impact;
                storyData = data;
            }
            public override NodeState Evaluate()
            {
                if(!_performed)
                {
                    storyData.Value += impact;
                    _performed = true;
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
                return StoryDatastore.Instance.GoodSoup.Value;
            }
        }
    }
}
