using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
	void OnApplicationQuit()
	{
		Globals.SaveGame();
		Steamworks.SteamClient.Shutdown();
	}
}
