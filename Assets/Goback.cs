
using UnityEngine;
using static MainSceneLoading;
public class Goback : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            MainSceneLoading.Instance.SwitchAmberRooms(AmberRoom.HALLWAY);
        }
    }
}
