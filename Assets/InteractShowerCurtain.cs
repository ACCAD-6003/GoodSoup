using Assets.Scripts.Objects.Interactions;
using UnityEngine;
public class InteractShowerCurtain : Interaction
{
    [SerializeField] InteractionObjectState stateOne, stateTwo;
    private void Awake()
    {

    }
    private void RefreshState()
    {
        stateOne.SetState(StoryDatastore.Instance.ShowerCurtainsOpen.Value);
        stateTwo.SetState(!StoryDatastore.Instance.ShowerCurtainsOpen.Value);
    }

    public override void DoAction()
    {
        StoryDatastore.Instance.ShowerCurtainsOpen.Value = !StoryDatastore.Instance.ShowerCurtainsOpen.Value;
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
