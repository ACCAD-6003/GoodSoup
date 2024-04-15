using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class Cutscene : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    private void Awake()
    {
        player.loopPointReached += StartGame;
    }
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            StartGame(null);
        }
    }
    void StartGame(VideoPlayer source) {
        player.loopPointReached -= StartGame;
        SceneManager.LoadScene("Start");
    }
}
