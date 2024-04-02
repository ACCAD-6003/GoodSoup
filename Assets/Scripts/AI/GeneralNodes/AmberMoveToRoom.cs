using BehaviorTree;
using static MainSceneLoading;

namespace Assets.Scripts.AI
{
    internal class AmberMoveToRoom : Node
    {
        private bool _switchedRooms = false;
        private AmberRoom _newRoom;
        public AmberMoveToRoom(AmberRoom newRoom)
        {
            _newRoom = newRoom;
        }
        public override NodeState Evaluate() {
            if (!_switchedRooms)
            {
                _switchedRooms = true;
                MainSceneLoading.Instance.SwitchAmberRooms(_newRoom);
            }
            return NodeState.SUCCESS;
        }
    }
}