using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseEnding : SerializedMonoBehaviour
{
    public Dictionary<Ending, GameObject> endingGameObjs;
    private Dictionary<Ending, EndingStars> starAssigner = new();

    private void Awake()
    {
        ConstructStarAssigner();
        endingGameObjs[StoryDatastore.Instance.ChosenEnding.Value].SetActive(true);
        GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");

        var ending = StoryDatastore.Instance.ChosenEnding.Value;
        if (!Globals.UnlockedEndings.ContainsKey(ending))
        {
            Globals.UnlockedEndings.Add(ending, starAssigner[ending].GetStars());
        }
        else
        {
            int stars = starAssigner[ending].GetStars();
            if (stars > Globals.UnlockedEndings[ending])
            {
                Globals.UnlockedEndings[ending] = stars;
                // new high score text or smth??
            }
        }

        foreach (GameObject obj in dontDestroyObjects)
        {
            Destroy(obj);
        }
    }
    void ConstructStarAssigner() {
        // Create star assigner dictionary
        starAssigner.Add(Ending.PARANOID, new StatBasedEnding(StoryDatastore.Instance.Paranoia, 3f));
        starAssigner.Add(Ending.GOOD_ENDING, new StatBasedEnding(StoryDatastore.Instance.Happiness, 7f));
        starAssigner.Add(Ending.BAD_ENDING, new StatBasedEnding(StoryDatastore.Instance.Annoyance, 7f));
        starAssigner.Add(Ending.GOOD_SOUP, new InstantFiveStarEnding());
        starAssigner.Add(Ending.KICKED_OUT_OF_COLLEGE, new InstantFiveStarEnding());
        starAssigner.Add(Ending.FAR_CRY, new InstantFiveStarEnding());
        // change to be condition based or something
        starAssigner.Add(Ending.ACADEMIC_WEAPON, new InstantFiveStarEnding());
        //starAssigner.Add(Ending.ACADEMIC_WEAPON, new StatBasedEnding(StoryDatastore.Instance.Paranoia, 3f));
        //starAssigner.Add(Ending.PARANOID, new StatBasedEnding(StoryDatastore.Instance.Paranoia, 3f));
        //starAssigner.Add(Ending.PARANOID, new StatBasedEnding(StoryDatastore.Instance.Paranoia, 3f));
        //starAssigner.Add(Ending.PARANOID, new StatBasedEnding(StoryDatastore.Instance.Paranoia, 3f));
    }
    class InstantFiveStarEnding : EndingStars { 
        public int GetStars() {
            return 5;
        }
    }
    class StatBasedEnding : EndingStars {
        StoryData<float> data;
        float valueToGetFiveStars;
        public StatBasedEnding(StoryData<float> data, float valueToGetFiveStars) {
            this.data = data;
            this.valueToGetFiveStars = valueToGetFiveStars;
        }
        public int GetStars() {
            int stars = Mathf.FloorToInt((data.Value / valueToGetFiveStars) * 5f);
            stars = Mathf.Max(stars, 1);
            stars = Mathf.Min(stars, 5);
            return stars;
        }
    }

    interface EndingStars {
        int GetStars();
    }

}
