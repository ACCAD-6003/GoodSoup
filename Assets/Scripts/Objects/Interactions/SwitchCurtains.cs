using Assets.Scripts.Objects.Interactions;
using UnityEngine;
public class SwitchCurtains : Interaction
{
    [SerializeField] InteractionObjectState stateOne, stateTwo;
    private void Awake()
    {
    }
    private void RefreshState() {
        stateOne.SetState(StoryDatastore.Instance.CurtainsOpen.Value);
        stateTwo.SetState(!StoryDatastore.Instance.CurtainsOpen.Value);
    }

    public override void DoAction()
    {
        StoryDatastore.Instance.CurtainsOpen.Value = !StoryDatastore.Instance.CurtainsOpen.Value;
        RefreshState();
        EndAction();
    }

    public override void LoadData(StoryDatastore data)
    {
        RefreshState();
    }

    public override void SaveData(StoryDatastore data)
    {

    }
}
