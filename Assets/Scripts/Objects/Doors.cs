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
            Debug.Log("This is the door i am looking for : " + StoryDatastore.Instance.CurrentAmberRoom.Value);
            foreach (KeyValuePair<AmberRoom, tile> pair in doors) {
                Debug.Log(pair.Key + " key  value" + pair.Value);
            }
            if (doors.ContainsKey(StoryDatastore.Instance.CurrentAmberRoom.Value))
            {
                Debug.Log("ENTRANCE SET TO: " + StoryDatastore.Instance.CurrentAmberRoom.Value);
                StoryDatastore.Instance.EntryDoor.Value = StoryDatastore.Instance.CurrentAmberRoom.Value;
                StoryDatastore.Instance.CurrentAmberRoom.Value = MainSceneLoading.Instance.CurrAdditiveScene;
            }
        }
        public void Start()
        {
            FindObjectOfType<grid_manager>().FixCharacterBetweenScenes();
        }
        public Dictionary<AmberRoom, tile> doors;
        public tile Entrance { get { return doors[StoryDatastore.Instance.EntryDoor.Value]; } }
    }
}
