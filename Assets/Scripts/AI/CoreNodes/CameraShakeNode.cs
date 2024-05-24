using BehaviorTree;

class CameraShakeNode : IEvaluateOnce
{
    CameraShake _shake;
    void Awake()
    {
        _shake = FindObjectOfType<CameraShake>();
    }
    public override void Run()
    {
        _shake.ShakeCamera();
    }
}