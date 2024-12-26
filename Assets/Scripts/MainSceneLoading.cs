using BehaviorTree;
using Sirenix.OdinInspector;
using System;
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
        { AmberRoom.GONE, "AtSchool" },
        { AmberRoom.HALLWAY, "Hallway" }
    };
    public enum AmberRoom { BEDROOM, BATHROOM, KITCHEN, HALLWAY, LIVING_ROOM, GONE }
    private static MainSceneLoading instance;
    public AmberRoom CurrAdditiveScene;
    public AmberRoom DefaultStartingScene = AmberRoom.BEDROOM;
    public GameObject LoadingScreen;

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
    private void Start()
    {
        SceneManager.LoadScene(rooms[DefaultStartingScene], LoadSceneMode.Additive);
        CurrAdditiveScene = DefaultStartingScene;
    }
    public void SwitchAmberRooms(AmberRoom room)
    {
        foreach(Interaction interaction in FindObjectsOfType<Interaction>()) {
            interaction.SaveData(StoryDatastore.Instance);
        }
        if (room == AmberRoom.GONE)
        {
            StoryDatastore.Instance.CurrentGamePhase.Value = GamePhase.AMBER_GONE;
            StoryDatastore.Instance.EntryDoor.Value = AmberRoom.GONE;
        }
        else if (StoryDatastore.Instance.CurrentGamePhase.Value == GamePhase.AMBER_GONE) {
            StoryDatastore.Instance.CurrentGamePhase.Value = GamePhase.AMBER_BACK;
        }
        if (room == AmberRoom.KITCHEN && StoryDatastore.Instance.CurrentGamePhase.Value == GamePhase.AMBER_BACK) {
            StoryDatastore.Instance.EntryDoor.Value = AmberRoom.HALLWAY;
        }
        if (room == AmberRoom.HALLWAY) {
            StoryDatastore.Instance.HallwayVisits.Value++;
        }
        switch (StoryDatastore.Instance.CurrentGamePhase.Value)
        {
            case GamePhase.TUTORIAL_BEDROOM:
                StoryDatastore.Instance.CurrentGamePhase.Value = GamePhase.BEFORE_AMBER_LEAVES;
                break;
            case GamePhase.AMBER_GONE:
                break;
        }

        StartCoroutine(UnloadAndLoadSceneAsync(room));
    }

	private IEnumerator UnloadAndLoadSceneAsync(AmberRoom room)
    {
        var fader = GameObject.FindAnyObjectByType<Fadeout>();
        var bt = GameObject.FindAnyObjectByType<BehaviorTree.Tree>();
		if (fader)
        {
            // stop amber from continuing routine
            Debug.Log("Fading out");
            Destroy(bt);
            fader.RunAnim();
			yield return new WaitForSeconds(1.25f);
		}

		else
        {
			Debug.LogWarning("FadeOut object not found. Please ensure it's in the active scene.");
		}

		var unloadOperation = SceneManager.UnloadSceneAsync(rooms[CurrAdditiveScene]);
        CurrAdditiveScene = room;
        unloadOperation.completed += (AsyncOperation operation) => {
            SceneManager.LoadScene(rooms[room], LoadSceneMode.Additive);
        };
    }
}
