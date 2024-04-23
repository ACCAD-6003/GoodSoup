using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
            StoryDatastore.Instance.DestroyStoryData();
            foreach (GameObject obj in dontDestroyObjects)
            {
                Destroy(obj);
            }
            SceneManager.LoadScene("Title Screen");
        }
    }
}
