using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) 
        {
            GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
            foreach (GameObject obj in dontDestroyObjects)
            {
                Destroy(obj);
            }
            SceneManager.LoadScene("Title Screen");
        }
    }
}
