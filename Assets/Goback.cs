using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainSceneLoading;
using static UnityEditor.Recorder.OutputPath;

public class Goback : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            MainSceneLoading.Instance.SwitchAmberRooms(AmberRoom.BEDROOM);
        }
    }
}
