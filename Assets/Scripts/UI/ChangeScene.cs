using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SwitchScene(string sceneName) {
        if (EndingSetup.timesBeaten == 9) {
            StoryDatastore.Instance.ChosenEnding.Value = Ending.TOUCH_GRASS;
            SceneManager.LoadScene("Endings");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
