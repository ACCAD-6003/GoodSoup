using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHider : MonoBehaviour
{
    // Reference to the object to enable/disable
    public GameObject targetObject;
    private List<string> rooms = new List<string>() { "Bedroom", "Kitchen", "Bathroom", "Hallway", "AmberGone" };
    void Update()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded && rooms.Contains(scene.name))
            {
                targetObject.SetActive(false);
                return;
            }
        }
        targetObject.SetActive(true);
    }
}