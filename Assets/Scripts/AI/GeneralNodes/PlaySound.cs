using BehaviorTree;
using UnityEngine;
public class PlaySound : Node
{
    bool done = false;
    AudioSource src;
    AudioClip clip;
    public PlaySound(AudioSource src, AudioClip clip)
    {
        this.src = src;
        this.clip = clip;
    }
    public override NodeState Evaluate()
    {
        if (!done)
        {
            done = true;
            src.PlayOneShot(clip);
        }
        return NodeState.SUCCESS;
    }
}