using BehaviorTree;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoStraightToEnding : IEvaluateOnce
{
    public Ending ending;
    public override void Run()
    {
        StoryDatastore.Instance.ChosenEnding.Value = ending;
        SceneManager.LoadScene("Endings");
    }
}