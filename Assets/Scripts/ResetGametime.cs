using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ResetGametime : MonoBehaviour
{
    void Awake()
    {
        StoryDatastore.Instance.GameTimeSpeedIndex = 0;
        Time.timeScale = 1f;
	}
}
