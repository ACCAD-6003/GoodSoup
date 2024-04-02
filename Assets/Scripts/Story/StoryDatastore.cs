using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.UI.UIElements;
using static ComputerEmail;
using static ComputerHUD;
using static MainSceneLoading;
public enum GamePhase { TUTORIAL_BEDROOM, BEFORE_AMBER_LEAVES, AMBER_GONE, AMBER_BACK }
public class StoryDatastore : MonoBehaviour
{
    private static StoryDatastore instance;

    public static StoryDatastore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StoryDatastore>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("StoryDatastore");
                    instance = singletonObject.AddComponent<StoryDatastore>();
                }
            }

            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    public Dictionary<int, (Vector3 location, Quaternion rotation)> BooksDropped = new Dictionary<int, (Vector3 location, Quaternion rotation)>();
    [SerializeField]
    public StoryData<bool> AnyBookDropped = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<float> BurnerHeat = new StoryData<float>(100);
    [SerializeField]
    public StoryData<bool> CurtainsOpen = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<EmailState> EmailState = new StoryData<EmailState>(ComputerHUD.EmailState.NOTHING_CHANGED);
    [SerializeField]
    public StoryData<bool> AwaitingEmailReply = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<DateTime> EmailSentTime;
    [SerializeField]
    public StoryData<AmberRoom> CurrentAmberRoom = new StoryData<AmberRoom>(AmberRoom.BEDROOM);
    [SerializeField]
    public StoryData<GamePhase> CurrentGamePhase = new StoryData<GamePhase>(GamePhase.TUTORIAL_BEDROOM);
    [SerializeField]
    public StoryData<float> ShowerTemperature = new StoryData<float>(40f);
    [SerializeField]
    public StoryData<float> HotShowerDuration = new StoryData<float>(0f);
    [SerializeField]
    public StoryData<bool> DoneShowering = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<MirrorState> MirrorState = new StoryData<MirrorState>(global::MirrorState.NOT_FOGGED);
    [SerializeField]
    public StoryData<bool> SinkRoutineDone = new StoryData<bool>(false);
    [SerializeField]
    public Dictionary<int, StoryData<bool>> MoveObjects = new Dictionary<int, StoryData<bool>>();
}
