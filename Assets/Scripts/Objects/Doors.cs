using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using static MainSceneLoading;

namespace Assets.Scripts.Objects
{
    public class Doors : SerializedMonoBehaviour
    {
        public void Awake()
        {
            if (doors.ContainsKey(StoryDatastore.Instance.CurrentAmberRoom.Value))
            {
                StoryDatastore.Instance.EntryDoor.Value = StoryDatastore.Instance.CurrentAmberRoom.Value;
                StoryDatastore.Instance.CurrentAmberRoom.Value = MainSceneLoading.Instance.CurrAdditiveScene;
            }
        }
        public void Start()
        {
            FindObjectOfType<grid_manager>().FixCharacterBetweenScenes();
        }
        public Dictionary<AmberRoom, tile> doors;
        public tile Entrance { get {
                if (StoryDatastore.Instance.CurrentGamePhase.Value == GamePhase.TUTORIAL_BEDROOM) {
                    return GameObject.Find("tile_7_3").GetComponent<tile>();
                } 
                return doors[StoryDatastore.Instance.EntryDoor.Value]; 
            } 
        }
    }
}
