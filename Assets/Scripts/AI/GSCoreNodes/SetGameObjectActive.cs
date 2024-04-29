using BehaviorTree;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class SetGameObjectActive : IEvaluateOnce
    {
        public GameObject GameObject;
        public bool ShouldSetActive;
        public override void Run() {
            GameObject.SetActive(ShouldSetActive);
        }
    }
}