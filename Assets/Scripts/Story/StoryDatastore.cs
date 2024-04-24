using System;
using System.Collections.Generic;
using UnityEngine;
using static AmberVisual;
using static Assets.Scripts.UI.UIElements;
using static ComputerEmail;
using static ComputerHUD;
using static MainSceneLoading;
public enum GamePhase { TUTORIAL_BEDROOM, BEFORE_AMBER_LEAVES, AMBER_GONE, AMBER_BACK, SLEEP_TIME }
public enum HeatSetting { LOW_TEMP, MEDIUM_TEMP, HIGH_TEMP }
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

    public void DestroyStoryData() {
        instance = null;
        Destroy(gameObject);
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
    public StoryData<DateTime> EmailSentTime = new StoryData<DateTime>(DateTime.MaxValue);
    [SerializeField]
    public StoryData<AmberRoom> CurrentAmberRoom = new StoryData<AmberRoom>(AmberRoom.BEDROOM);
    [SerializeField]
    public StoryData<GamePhase> CurrentGamePhase = new StoryData<GamePhase>(GamePhase.TUTORIAL_BEDROOM);
    [SerializeField]
    public StoryData<float> ShowerTemperature = new StoryData<float>(40f);
    [SerializeField]
    public StoryData<bool> DoneShowering = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<MirrorState> MirrorState = new StoryData<MirrorState>(global::MirrorState.NOT_FOGGED);
    [SerializeField]
    public StoryData<bool> SinkRoutineDone = new StoryData<bool>(false);
    [SerializeField]
    public Dictionary<int, StoryData<bool>> MoveObjects = new Dictionary<int, StoryData<bool>>();
    [SerializeField]
    public StoryData<bool> ResultOfEvaluation = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<float> Paranoia = new StoryData<float>(0f);
    [SerializeField]
    public StoryData<float> Annoyance = new StoryData<float>(0f);
    [SerializeField]
    public StoryData<float> Happiness = new StoryData<float>(0f);
    [SerializeField]
    public StoryData<Ending> ChosenEnding = new StoryData<Ending>(Ending.FAR_CRY);
    [SerializeField]
    public StoryData<bool> AmberDressed = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<ClothingOption> ChosenClothing = new StoryData<ClothingOption>(ClothingOption.Dirty);
    [SerializeField]
    public Dictionary<int, StoryData<ClothingOption>> DisplayedShirts = new Dictionary<int, StoryData<ClothingOption>>();
    [SerializeField]
    public StoryData<bool> ShirtPickedUp = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<bool> BooksBlown = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<bool> PickedUpBackpack = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<ClothingOption> AmberWornClothing = new StoryData<ClothingOption>(ClothingOption.Pajamas);
    [SerializeField]
    public StoryData<HairOption> AmberHairOption = new StoryData<HairOption>(HairOption.CLEAN);
    [SerializeField]
    public StoryData<bool> PlayerTurnedShowerOff = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<bool> RadiatorHot = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<float> HotShowerDuration = new StoryData<float>(0f);
    [SerializeField]
    public StoryData<bool> ShowerCurtainsOpen = new StoryData<bool>(true);
    [SerializeField]
    public StoryData<bool> TowelHot = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<bool> GoodSoupPuzzleSolved = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<AmberRoom> EntryDoor = new StoryData<AmberRoom>(AmberRoom.BEDROOM);
    [SerializeField]
    public StoryData<int> HallwayVisits = new(0);
    [SerializeField]
    public StoryData<bool> AmberPickedUpKey = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<bool> WearingChefHat = new(false);
    [SerializeField]
    public StoryData<FaceOption> FaceOption = new(AmberVisual.FaceOption.GROGGY);
    [SerializeField]
    public StoryData<HeatSetting> HeatSetting = new(global::HeatSetting.LOW_TEMP);
    [SerializeField]
    public StoryData<float> FoodQuality = new(0f);
    [SerializeField]
    public StoryData<bool> ActivelyCooking = new(false);
    [SerializeField]
    public StoryData<float> Salt = new(0f);
    [SerializeField]
    public StoryData<float> Pepper = new(0f);
}
