using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSetup : SerializedMonoBehaviour
{
    public EndingsContent content;
    public Dictionary<Ending, GameObject> endingGameObjs;
    private Dictionary<Ending, EndingStars> starAssigner = new();
    public TextMeshProUGUI endingTitle, endingDescription, starPerformance, highScoreText, playAgain;
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
    [SerializeField] Image background;
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
        catch (Exception e) { 
        
        }

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
            else {
                highScoreText.text = $"Did not surpass high score of {Globals.UnlockedEndings[ending]} stars.";
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


        foreach (GameObject obj in dontDestroyObjects)
        {
            Destroy(obj);
        }

        background.color = GetAverageColor(endingImage.sprite.texture);
    }
    private void Start()
    {
        StartCoroutine(ShowEndingRoutine());
    }

    Color GetAverageColor(Texture2D tex)
    {
        Color[] pixels = tex.GetPixels();

        float r = 0, g = 0, b = 0;
        int count = 0;

        foreach (Color pixel in pixels)
        {
            r += pixel.r;
            g += pixel.g;
            b += pixel.b;
            count++;
        }

        return new Color(r / count, g / count, b / count);
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
        endingImage.transform.localScale = Vector3.zero;
        playAgain.transform.localScale = Vector3.zero;

        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(endingImage.transform, 10f, 0.25f, 9f, 0.25f));
        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(endingTitle.transform, 1.25f, 0.25f, 1f, 0.25f));
        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(endingDescription.transform, 1.1f, 0.25f, 1f, 0.25f));
        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(starDisplayerParentTransform, 1.1f, 0.25f, 1f, 0.25f));
        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(starDisplayer.ShowStarsRoutine(stars));
        yield return new WaitForSeconds(0.25f);
        if (doingHighScoreAnimation) {
            src.PlayOneShot(whoosh);
            yield return StartCoroutine(PullDownAndHighScore(0.5f, -276f, 23.4754f, 161.2058f));
            src.PlayOneShot(whoosh);
            yield return StartCoroutine(EnlargeAndShrinkTransform(highScoreText.transform, 1.1f, 0.25f, 1f, 0.25f));
            yield return new WaitForSeconds(0.25f);
        }
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(starPerformance.transform, 1.1f, 0.25f, 1f, 0.25f));
        yield return new WaitForSeconds(0.25f);
        src.PlayOneShot(whoosh);
        yield return StartCoroutine(EnlargeAndShrinkTransform(playAgain.transform, 1.1f, 0.25f, 1f, 0.25f));
        yield return new WaitForSeconds(0.25f);
/*        StoryDatastore.Instance.ChosenEnding.Value = StoryDatastore.Instance.ChosenEnding.Value + 1;
        SceneManager.LoadScene("Endings");*/
    }
    private IEnumerator PullDownAndHighScore(float duration, float targetPosY1, float targetPosY2, float targetHeight2)
    {
        float elapsedTime = 0.0f;
        Vector2 startPos1 = starTransform.anchoredPosition;
        Vector2 startPos2 = starBgImageTransform.anchoredPosition;
        float startHeight2 = starBgImageTransform.sizeDelta.y;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float sineT = Mathf.Sin(t * Mathf.PI * 0.5f); // Using sine function for easing out

            starTransform.anchoredPosition = Vector2.Lerp(startPos1, new Vector2(startPos1.x, targetPosY1), sineT);
            starBgImageTransform.anchoredPosition = Vector2.Lerp(startPos2, new Vector2(startPos2.x, targetPosY2), sineT);
            starBgImageTransform.sizeDelta = new Vector2(starBgImageTransform.sizeDelta.x, Mathf.Lerp(startHeight2, targetHeight2, sineT));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position is reached
        starTransform.anchoredPosition = new Vector2(startPos1.x, targetPosY1);
        starBgImageTransform.anchoredPosition = new Vector2(startPos2.x, targetPosY2);
        starBgImageTransform.sizeDelta = new Vector2(starBgImageTransform.sizeDelta.x, targetHeight2);
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
