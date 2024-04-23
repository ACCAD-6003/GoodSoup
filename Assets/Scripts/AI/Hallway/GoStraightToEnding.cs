using BehaviorTree;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoStraightToEnding : Node
{
    bool done = false;
    Ending ending;
    public GoStraightToEnding(Ending ending)
    {
        this.ending = ending;
    }
    public override NodeState Evaluate()
    {
        if (!done)
        {
            done = true;
            StoryDatastore.Instance.ChosenEnding.Value = ending;
            SceneManager.LoadScene("Endings");
        }
        return NodeState.SUCCESS;
    }
}