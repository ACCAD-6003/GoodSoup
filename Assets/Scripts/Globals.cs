using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Globals {
    public const float HEAT_THRESHOLD = 150;
    public const float PREFERABLE_HEAT = 100;
    public const float SECONDS_BETWEEN_EMAIL_REPLIES = 10f;
    public const float SECONDS_AMBER_NEEDS_TO_SHOWER_IN_WARM_WATER = 6.5f;
    public const float FLUSH_SHOWER_TEMP_IMPACT = -30f;
    public const float AMBER_GETS_COLD_TEMP = 70f;
    public const float TEMP_INCREASE_MODIFIER = 8 * 3f;
    public const float TEMP_DECREASE_MODIFIER = 2f;
    public const float AMBER_PREFERABLE_SHOWER_TEMP = 100f;

    public static Dictionary<Ending, int> UnlockedEndings = new();
    public static List<Ending> EndingHintChecked = new();
	public static bool StopwatchUnlocked = false;
	public static float Volume = 0.5f;

    public static Ending LastEnding;
    public static bool FirstTitleScreen = true;

	private static string SaveFilePath => Path.Combine(Application.persistentDataPath, "gamedata.json");
	public static void SaveGame()
	{
		GameData data = new GameData
		{
			UnlockedEndings = Globals.UnlockedEndings,
			EndingHintChecked = Globals.EndingHintChecked,
			StopwatchUnlocked = Globals.StopwatchUnlocked,
			Volume = Globals.Volume
		};

		string json = JsonConvert.SerializeObject(data);
		Debug.Log($"Saving data to persistent data path {Application.persistentDataPath}");
		File.WriteAllText(SaveFilePath, json);
	}

	public static void LoadGame()
	{
		if (File.Exists(SaveFilePath))
		{
			Debug.Log($"Loading file from persistent data path {Application.persistentDataPath}");
			string json = File.ReadAllText(SaveFilePath);
			GameData data = JsonConvert.DeserializeObject<GameData>(json);

			Globals.UnlockedEndings = data.UnlockedEndings;
			Globals.EndingHintChecked = data.EndingHintChecked;
			Globals.StopwatchUnlocked = data.StopwatchUnlocked;
			Globals.Volume = data.Volume;
		}
		else
		{
			Debug.Log("No save file found.");
		}
	}

	[Serializable]
	public class GameData
	{
		public Dictionary<Ending, int> UnlockedEndings;
		public List<Ending> EndingHintChecked;
		public bool StopwatchUnlocked;
		public float Volume;	
	}

}