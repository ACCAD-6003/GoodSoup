using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseEnding : SerializedMonoBehaviour
{
    public Dictionary<Ending, GameObject> endingGameObjs;
    void Awake()
    {
        endingGameObjs[StoryDatastore.Instance.ChosenEnding.Value].SetActive(true);
        GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");

        foreach (GameObject obj in dontDestroyObjects)
        {
            Destroy(obj);
        }
    }
}
