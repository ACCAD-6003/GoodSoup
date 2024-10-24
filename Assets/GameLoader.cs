using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    void Awake()
    {
        if (Globals.FirstTitleScreen) {
			Globals.LoadGame();
		}
        try {
           Steamworks.SteamClient.Init(2965780);
            Debug.Log("Steam name is" + Steamworks.SteamClient.Name);
        }  
        catch {
            Debug.Log("Failed to connect to steam");
        }
    }
}
