using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static AmberVisual;
using static ComputerHUD;
using static MainSceneLoading;
public enum GamePhase { TUTORIAL_BEDROOM, BEFORE_AMBER_LEAVES, AMBER_GONE, AMBER_BACK, SLEEP_TIME }
public enum HeatSetting { LOW_TEMP, MEDIUM_TEMP, HIGH_TEMP }
public class StoryDatastore : MonoBehaviour
{
    private static StoryDatastore instance;

    public static StoryDatastore Instance { get => instance; private set => instance = value; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        CreateStoryDataContainer();

        var enumCount = (int)Enum.GetValues(typeof(StoryDataType)).Cast<StoryDataType>().Max();
        if (StoryDataContainer.Count - 1 != enumCount)
        {
            Debug.LogError("The StoryDataContainer dictionary does not have the same number of entries as the StoryDataType dictionary. You probably forgot to add an entry to the dictionary.");
        }
    }

    public void DestroyStoryData()
    {
        instance = null;
        Destroy(gameObject);
    }
    private void CreateStoryDataContainer() {
        StoryDataContainer = new Dictionary<StoryDataType, IStoryData>() {
            { StoryDataType.AnyBookDropped, Instance.AnyBookDropped },
            { StoryDataType.BurnerHeat, Instance.BurnerHeat },
            { StoryDataType.CurtainsOpen, Instance.CurtainsOpen },
            { StoryDataType.EmailState, Instance.EmailState },
            { StoryDataType.AwaitingEmailReply, Instance.AwaitingEmailReply },
            { StoryDataType.EmailSentTime, Instance.EmailSentTime },
            { StoryDataType.CurrentAmberRoom, Instance.CurrentAmberRoom },
            { StoryDataType.CurrentGamePhase, Instance.CurrentGamePhase },
            { StoryDataType.ShowerTemperature, Instance.ShowerTemperature },
            { StoryDataType.DoneShowering, Instance.DoneShowering },
            { StoryDataType.MirrorState, Instance.MirrorState },
            { StoryDataType.SinkRoutineDone, Instance.SinkRoutineDone },
            { StoryDataType.ResultOfEvaluation, Instance.ResultOfEvaluation },
            { StoryDataType.Paranoia, Instance.Paranoia },
            { StoryDataType.Annoyance, Instance.Annoyance },
            { StoryDataType.Happiness, Instance.Happiness },
            { StoryDataType.ChosenEnding, Instance.ChosenEnding },
            { StoryDataType.AmberDressed, Instance.AmberDressed },
            { StoryDataType.ChosenClothing, Instance.ChosenClothing },
            { StoryDataType.ShirtPickedUp, Instance.ShirtPickedUp },
            { StoryDataType.BooksBlown, Instance.BooksBlown },
            { StoryDataType.PickedUpBackpack, Instance.PickedUpBackpack },
            { StoryDataType.AmberWornClothing, Instance.AmberWornClothing },
            { StoryDataType.AmberHairOption, Instance.AmberHairOption },
            { StoryDataType.PlayerTurnedShowerOff, Instance.PlayerTurnedShowerOff },
            { StoryDataType.RadiatorHot, Instance.RadiatorHot },
            { StoryDataType.HotShowerDuration, Instance.HotShowerDuration },
            { StoryDataType.ShowerCurtainsOpen, Instance.ShowerCurtainsOpen },
            { StoryDataType.TowelHot, Instance.TowelHot },
            { StoryDataType.GoodSoupPuzzleSolved, Instance.GoodSoupPuzzleSolved },
            { StoryDataType.EntryDoor, Instance.EntryDoor },
            { StoryDataType.HallwayVisits, Instance.HallwayVisits },
            { StoryDataType.AmberPickedUpKey, Instance.AmberPickedUpKey },
            { StoryDataType.WearingChefHat, Instance.WearingChefHat },
            { StoryDataType.FaceOption, Instance.FaceOption },
            { StoryDataType.HeatSetting, Instance.HeatSetting },
            { StoryDataType.FoodQuality, Instance.FoodQuality },
            { StoryDataType.ActivelyCooking, Instance.ActivelyCooking },
            { StoryDataType.AmberOutOfBed, Instance.AmberOutOfBed }
        };
    }
    [Header("This is a global setting that modifies the amount of time amber waits between actions.")]
    public float AmberTimeModifier = 1f;

    // Not story data
    [SerializeField]
    public Dictionary<int, (Vector3 location, Quaternion rotation)> BooksDropped = new Dictionary<int, (Vector3 location, Quaternion rotation)>();
    [SerializeField]
    public Dictionary<int, StoryData<ClothingOption>> DisplayedShirts = new Dictionary<int, StoryData<ClothingOption>>();
    [SerializeField]
    public Dictionary<int, StoryData<bool>> MoveObjects = new Dictionary<int, StoryData<bool>>();
    [SerializeField]
    public int GameTimeSpeedIndex = 0;

    // Story data
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
    public StoryData<ClothingOption> ChosenClothing = new StoryData<ClothingOption>(ClothingOption.DIRTY);
    [SerializeField]
    public StoryData<bool> ShirtPickedUp = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<bool> BooksBlown = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<bool> PickedUpBackpack = new StoryData<bool>(false);
    [SerializeField]
    public StoryData<ClothingOption> AmberWornClothing = new StoryData<ClothingOption>(ClothingOption.PAJAMAS);
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
    public StoryData<bool> AmberOutOfBed = new StoryData<bool>(false);

    public enum StoryDataType
    {
        AnyBookDropped,
        BurnerHeat,
        CurtainsOpen,
        EmailState,
        AwaitingEmailReply,
        EmailSentTime,
        CurrentAmberRoom,
        CurrentGamePhase,
        ShowerTemperature,
        DoneShowering,
        MirrorState,
        SinkRoutineDone,
        ResultOfEvaluation,
        Paranoia,
        Annoyance,
        Happiness,
        ChosenEnding,
        AmberDressed,
        ChosenClothing,
        ShirtPickedUp,
        BooksBlown,
        PickedUpBackpack,
        AmberWornClothing,
        AmberHairOption,
        PlayerTurnedShowerOff,
        RadiatorHot,
        HotShowerDuration,
        ShowerCurtainsOpen,
        TowelHot,
        GoodSoupPuzzleSolved,
        EntryDoor,
        HallwayVisits,
        AmberPickedUpKey,
        WearingChefHat,
        FaceOption,
        HeatSetting,
        FoodQuality,
        ActivelyCooking,
        AmberOutOfBed
    }

    private Dictionary<StoryDataType, IStoryData> StoryDataContainer;

    private Dictionary<Type, Func<string, dynamic>> Deserializer = new Dictionary<Type, Func<string, dynamic>>() {
        { typeof(bool), (value) => DeserializeToBool(value) },
        { typeof(int), (value) => DeserializeToInt(value) },
        { typeof(float), (value) => DeserializeToFloat(value) },
    };
    public dynamic GetStoryDataObject(StoryDataType type)
    {
        if (Instance.StoryDataContainer == null)
        {
            Debug.LogError("Story data container is null in GetStoryDataObject.");
            return null;
        }

        if (!Instance.StoryDataContainer.ContainsKey(type))
        {
            Debug.LogError($"No story data exists in the StoryDataDictionary with the specified key {type}");
            return null;
        }

        return Instance.StoryDataContainer[type];
    }
    public dynamic GetStoryDataValue(StoryDataType type)
    {
        var storyDataObject = GetStoryDataObject(type);

        if (storyDataObject == null)
        {
            return null;
        }

        // Get the type of the value stored in the dictionary
        Type dataType = storyDataObject.GetDataType();
        // Use reflection to invoke the Value property of StoryData<> dynamically
        var valueProperty = typeof(StoryData<>).MakeGenericType(dataType).GetProperty("Value");

        return valueProperty.GetValue(storyDataObject);
    }
    public dynamic DeserializeStoryDataValue(StoryDataType type, string value) {
        if (!Instance.StoryDataContainer.ContainsKey(type))
        {
            Debug.LogError($"No story data exists in the StoryDataDictionary with the specified key {type}");
        }
        var storyDataType = Instance.StoryDataContainer[type].GetDataType();
        if (storyDataType.IsEnum) {
            return DeserializeEnum(storyDataType, value);
        }
        if (!Deserializer.ContainsKey(storyDataType)) {
            Debug.LogError($"No deserializer exists with the specified key {type}");
        }
        return Deserializer[storyDataType](value);
    }
    public void SetStoryDataValue(StoryDataType type, string value) {
        var storyDataValue = DeserializeStoryDataValue(type, value);
        Type dataType = Instance.StoryDataContainer[type].GetDataType();
        var valueProperty = typeof(StoryData<>).MakeGenericType(dataType).GetProperty("Value");
        valueProperty.SetValue(StoryDataContainer[type], storyDataValue);
    }
    private static bool DeserializeToBool(string value) {
        if (!value.ContainsInsensitive("true") && !value.ContainsInsensitive("false")) {
            Debug.LogError($"Attempted to deserialize a bool value of value {value} that wasn't true or false. You probably forgot to fill out a field.");
        }
        return value.ToUpper() == "TRUE";
    }
    private static int DeserializeToInt(string value)
    {
        int result;
        if (!int.TryParse(value, out result)) {
            Debug.Log($"Attempted to deserialize an integer of value {value} that wasn't a valid integer. You probably forgot to fill out a field.");
        }
        return result;
    }
    private static float DeserializeToFloat(string value) {
        float result;
        if (!float.TryParse(value, out result))
        {
            Debug.Log($"Attempted to deserialize a float with value {value} that wasn't a valid float. You probably forgot to fill out a field.");
        }
        return result;
    }
    private static dynamic DeserializeEnum(Type type, string value) {
        if (!type.IsEnum)
        {
            Debug.Log($"The provided type {type} is not an enum.");
        }
        return Enum.Parse(type, value);
    }
}
