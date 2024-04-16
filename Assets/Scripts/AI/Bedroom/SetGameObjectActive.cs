using BehaviorTree;
using UnityEngine;

namespace Assets.Scripts.AI
{
    internal class SetGameObjectActive : Node
    {
        private GameObject phoneOnTable;
        private bool active;
        private bool completed = false;
        public SetGameObjectActive(GameObject gameObject, bool v)
        {
            this.phoneOnTable = gameObject;
            this.active = v;
        }
        public override NodeState Evaluate() {
            if (!completed) {
                completed = true;
                phoneOnTable.SetActive(active);
            }
            state = NodeState.SUCCESS;
            return state;
        }
    }
}