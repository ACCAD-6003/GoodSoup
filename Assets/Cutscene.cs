using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class Cutscene : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    [SerializeField] string videoName;
    [SerializeField] string sceneNameToTransitionTo;
    private void Start()
    {
        if (player) {
            var path = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
            player.url = path;
            player.Play();
        }

        player.loopPointReached += StartGame;
    }
    void OnDestroy() {
        player.loopPointReached -= StartGame;
    }
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            StartGame(null);
        }
    }
    void StartGame(VideoPlayer source) {
        player.loopPointReached -= StartGame;
        SceneManager.LoadScene(sceneNameToTransitionTo);
    }
}
