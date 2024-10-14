using BehaviorTree;
using System.Collections;
using UnityEngine;
using static MainSceneLoading;

namespace Assets.Scripts.AI
{
    internal class AmberMoveToRoom : IEvaluateOnce
    {
        [SerializeField] AmberRoom _newRoom;
        public override void Run()
        {
            MainSceneLoading.Instance.SwitchAmberRooms(_newRoom);
        }
    }
}