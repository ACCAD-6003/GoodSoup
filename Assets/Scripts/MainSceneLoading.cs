using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoading : SerializedMonoBehaviour
{
    private Dictionary<AmberRoom, string> rooms = new() {
        { AmberRoom.BEDROOM, "Bedroom" },
        { AmberRoom.BATHROOM, "Bathroom" },
        { AmberRoom.KITCHEN, "Kitchen" },
        { AmberRoom.LIVING_ROOM, "LivingRoom" },
        { AmberRoom.GONE, "Gone" }
    };
    public enum AmberRoom { BEDROOM, BATHROOM, KITCHEN, LIVING_ROOM, GONE }
    private static MainSceneLoading instance;
    public AmberRoom CurrAdditiveScene;

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
        CurrAdditiveScene = AmberRoom.BEDROOM;

    }

    public void SwitchAmberRooms(AmberRoom room)
    {
        switch (StoryDatastore.Instance.CurrentGamePhase.Value)
        {
            case GamePhase.TUTORIAL_BEDROOM:
                StoryDatastore.Instance.CurrentGamePhase.Value = GamePhase.BEFORE_AMBER_LEAVES;
                break;
            case GamePhase.AMBER_GONE:
                break;
        }
        UnloadAndLoadSceneAsync(room);
    }

    private void UnloadAndLoadSceneAsync(AmberRoom room)
    {
        var unloadOperation = SceneManager.UnloadSceneAsync(rooms[CurrAdditiveScene]);
        unloadOperation.completed += (AsyncOperation operation) => {
            SceneManager.LoadScene(rooms[room]);
        };
    }
}
