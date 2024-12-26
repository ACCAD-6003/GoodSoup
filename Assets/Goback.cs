
using UnityEngine;
using static MainSceneLoading;
public class Goback : MonoBehaviour
{
    bool goingBack = false;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !goingBack) {
			goingBack = true;
			MainSceneLoading.Instance.SwitchAmberRooms(AmberRoom.HALLWAY);
        }
    }
}
