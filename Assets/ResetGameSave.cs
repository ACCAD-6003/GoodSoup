using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGameSave : MonoBehaviour
{
    public void ResetGame() {
        Globals.UnlockedEndings = new();
        Globals.EndingHintChecked = new();
        Globals.StopwatchUnlocked = false;
	    Globals.FirstTitleScreen = true;
        Globals.SaveGame();
        SceneManager.LoadScene("Title Screen");
    }
}
