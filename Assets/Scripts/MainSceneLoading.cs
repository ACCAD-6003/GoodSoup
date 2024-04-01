using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoading : MonoBehaviour
{
    private static MainSceneLoading instance;

    public static MainSceneLoading Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.LoadScene("Bedroom", LoadSceneMode.Additive);
    }
    public void LoadScene(string sceneName) { 
    
    }
}
