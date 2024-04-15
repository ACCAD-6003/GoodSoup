using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Cutscene : MonoBehaviour
{
    [SerializeField] CutsceneFrames frames;
    [SerializeField] Image image;
    [SerializeField] AudioSource src;
    private int frameIndex = 0;
    private void Awake()
    {
        PlayFrame();
    }
    public void NextFrame() {
        frameIndex++;
        PlayFrame();
    }
    void PlayFrame() {
        image.sprite = frames.frames[frameIndex].sprite;
        src.PlayOneShot(frames.frames[frameIndex].sound);
    }
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            StartGame();
        }
    }
    void StartGame() {
        SceneManager.LoadScene("Start");
    }
}
