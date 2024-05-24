using BehaviorTree;

class PutInProgress : IEvaluateOnce
{
    public bool ShouldPutInProgress;
    public Interaction _interaction;
    public PutInProgress(bool inProgress, Interaction interaction)
    {
        ShouldPutInProgress = inProgress;
        _interaction = interaction;
    }
    public override void Run()
    {
        if (ShouldPutInProgress)
        {
            _interaction.PutInProgress();
        }
        else
        {
            _interaction.EndAction();
        }
    }
}