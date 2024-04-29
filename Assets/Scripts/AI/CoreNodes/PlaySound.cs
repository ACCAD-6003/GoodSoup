using BehaviorTree;
using UnityEngine;
public class PlaySound : IEvaluateOnce
{
    public AudioSource src;
    public AudioClip clip;
    public override void Run()
    {
        src.PlayOneShot(clip);
    }
}