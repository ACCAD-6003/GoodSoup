using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using static MainSceneLoading;

namespace Assets.Scripts.Objects
{
    public class Doors : SerializedMonoBehaviour
    {
        [Header("This should only be used in the bedroom. It is the tile Amber will get off of the bed onto.")]
        public tile WakeUpTile;
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
                    return WakeUpTile;
                } 
                return doors[StoryDatastore.Instance.EntryDoor.Value]; 
            } 
        }
    }
}
