using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSetup : SerializedMonoBehaviour
{
    public bool debug = false;
	public EndingsContent content;
    public Dictionary<Ending, GameObject> endingGameObjs;
    private Dictionary<Ending, EndingStars> starAssigner = new();
    public TextMeshProUGUI endingTitle, endingDescription, starPerformance, highScoreText;
    public GameObject playAgain;
	public RectTransform starTransform, starBgImageTransform;
    public Color couldDoBetter, perfectPerformanceColor;
    public Image endingImage;
    public StarDisplayer starDisplayer;
    public Transform starDisplayerParentTransform;
    private int stars;
    public bool doingHighScoreAnimation = false;
    public static int timesBeaten = 0;
    public AudioSource src;
    public AudioClip whoosh;
	private void Awake()
    {

        timesBeaten++;
        ConstructStarAssigner();
        GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");

        var ending = StoryDatastore.Instance.ChosenEnding.Value;

        var achId = ending.ToString();
		var ach = new Steamworks.Data.Achievement(achId);
        try
        {
            if (!ach.State)
            {
                ach.Trigger();
            }
        }
        catch { }

        src.clip = content.EndingContent[ending].endingAudio;
        src.Play();

        stars = starAssigner[ending].GetStars();

        var endingContent = content.EndingContent[ending];
        endingTitle.text = endingContent.DisplayName;
        endingDescription.text = endingContent.Description;
        endingImage.sprite = endingContent.imageSprite;
        Globals.LastEnding = ending;
        if (!Globals.UnlockedEndings.ContainsKey(ending))
        {
            Globals.UnlockedEndings.Add(ending, stars);
        }
        else 
        {
            if (stars > Globals.UnlockedEndings[ending])
            {
                Globals.UnlockedEndings[ending] = stars;
                highScoreText.text = $"New high score of {stars} stars!";
                highScoreText.color = perfectPerformanceColor;
            }
            else if (stars != 5)
            {
                highScoreText.text = $"You did not surpass high score..";
                highScoreText.color = couldDoBetter;
            }
            else
            {
                highScoreText.text = "Maybe try for a different ending?";
				highScoreText.color = couldDoBetter;
			}
            doingHighScoreAnimation = true;
        }

        if (stars != 5)
        {
            starPerformance.text = endingContent.DoBetterMessage;
            starPerformance.color = couldDoBetter;
        }
        else {
            starPerformance.text = endingContent.FiveStarMessage;
            starPerformance.color = perfectPerformanceColor;
		}

        if (Globals.UnlockedEndings.Keys.Count == Enum.GetNames(typeof(Ending)).Length) 
        {
            bool allEndingsFiveStars = true;
			foreach (Ending e in Enum.GetValues(typeof(Ending)))
            {
				if (Globals.UnlockedEndings[e] != 5)
                {
					allEndingsFiveStars = false;
					break;
				}
			}
			if (allEndingsFiveStars)
			{
				var achievement = new Steamworks.Data.Achievement("ALL_STAR");
				try
				{
					if (!achievement.State)
					{
						achievement.Trigger();
					}
				}
				catch { }
			}
		}

        foreach (GameObject obj in dontDestroyObjects)
        {
            Destroy(obj);
        }

    }
    private void Start()
    {
        StartCoroutine(ShowEndingRoutine());
    }
    void ConstructStarAssigner() {
        // Create star assigner dictionary
        starAssigner.Add(Ending.PARANOID, new StatBasedEnding(StoryDatastore.Instance.Paranoia, 12f));
        starAssigner.Add(Ending.GOOD_ENDING, new StatBasedEnding(StoryDatastore.Instance.Happiness, 15f));
        starAssigner.Add(Ending.BAD_ENDING, new StatBasedEnding(StoryDatastore.Instance.Annoyance, 10f));

        starAssigner.Add(Ending.GOOD_SOUP, new InstantFiveStarEnding());
        starAssigner.Add(Ending.KICKED_OUT_OF_COLLEGE, new InstantFiveStarEnding());
        starAssigner.Add(Ending.FAR_CRY, new InstantFiveStarEnding());
        starAssigner.Add(Ending.ACADEMIC_WEAPON, new InstantFiveStarEnding());
        starAssigner.Add(Ending.LOCKED_OUT, new InstantFiveStarEnding());
        starAssigner.Add(Ending.TOUCH_GRASS, new InstantFiveStarEnding());
        starAssigner.Add(Ending.BURNT_DOWN, new InstantFiveStarEnding());
        starAssigner.Add(Ending.MID_SOUP, new InstantFiveStarEnding());
    }

    private IEnumerator ShowEndingRoutine()
    {
        endingTitle.transform.localScale = Vector3.zero;
        endingDescription.transform.localScale = Vector3.zero;
        starPerformance.transform.localScale = Vector3.zero;
        highScoreText.transform.localScale = Vector3.zero;
        playAgain.transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(endingTitle.transform, 1.25f, 0.25f, 1f, 0.25f));
        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(starDisplayerParentTransform, 1.1f, 0.25f, 1f, 0.25f));
        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(starDisplayer.ShowStarsRoutine(stars));
        yield return new WaitForSeconds(0.25f);
        if (doingHighScoreAnimation) {
            src.PlayOneShot(whoosh);
            yield return StartCoroutine(EnlargeAndShrinkTransform(highScoreText.transform, 1.1f, 0.25f, 1f, 0.25f));
            yield return new WaitForSeconds(0.25f);
        }
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(playAgain.transform, 1.1f, 0.25f, 1f, 0.25f));
        yield return new WaitForSeconds(0.25f);
        if(debug)
		{
			StoryDatastore.Instance.ChosenEnding.Value = StoryDatastore.Instance.ChosenEnding.Value + 1;
			SceneManager.LoadScene("Endings");
		}

	}
    public static IEnumerator EnlargeAndShrinkTransform(Transform transformObject, float enlargeScale, float enlargeDuration, float shrinkScale, float shrinkDuration)
    {
        yield return EndingSetup.ChangeScale(transformObject, enlargeScale, enlargeDuration);
        yield return EndingSetup.ChangeScale(transformObject, shrinkScale, shrinkDuration);
    }
    public static IEnumerator ChangeScale(Transform transformObject, float targetScale, float duration)
    {
        Vector3 initialScale = transformObject.localScale;
        Vector3 finalScale = new Vector3(targetScale, targetScale, targetScale);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = Mathf.Sin((elapsedTime / duration) * Mathf.PI * 0.5f);
            transformObject.localScale = Vector3.Lerp(initialScale, finalScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transformObject.localScale = finalScale;
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
