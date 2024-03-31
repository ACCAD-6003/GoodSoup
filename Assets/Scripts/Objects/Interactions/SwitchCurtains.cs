using Assets.Scripts.Objects.Interactions;
using UnityEngine;
public class SwitchCurtains : Interaction
{
    [SerializeField] StoryDatastore story;
    [SerializeField] InteractionObjectState stateOne, stateTwo;
    private StoryData<bool> _state;
    private void Start()
    {
        _state = story.CurtainsOpen;
        RefreshState();
    }
    private void RefreshState() {
        stateOne.SetState(_state.Value);
        stateTwo.SetState(!_state.Value);
    }
    override protected void DoAction()
    {
        _state.Value = !_state.Value;
        RefreshState();
        EndAction();
    }
}
