using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class EndingBookPage : MonoBehaviour
{
    public Ending correspondingEnding;
    public EndingsContent content;
    public Image endingImage;
    public TextMeshProUGUI title, descriptionText, hintText, endingInstructionsText;
    public Button revealButton;
    public List<Star> stars;
    EndingContent endingContent;
    void Awake()
    {
        endingContent = content.EndingContent[correspondingEnding];
        if (Globals.UnlockedEndings.ContainsKey(correspondingEnding))
        {
            title.text = endingContent.DisplayName;
            descriptionText.text = endingContent.Description;
            descriptionText.enabled = true;
            endingInstructionsText.enabled = true;
            endingImage.sprite = endingContent.imageSprite;
            revealButton.gameObject.SetActive(false);
            int stars = Globals.UnlockedEndings[correspondingEnding];
            if (stars == 5)
            {
                endingInstructionsText.text = endingContent.FiveStarMessage;
            }
            else {
                endingInstructionsText.text = endingContent.DoBetterMessage;
            }
            for (int i = 0; i < stars; i++)
            {
                this.stars[i].EnableStar();
            }
        }
        else if (Globals.EndingHintChecked.Contains(correspondingEnding))
        {
            DisplayHintScreen();
        }
        else
        {
            title.text = "???";
            descriptionText.enabled = false;
            endingInstructionsText.enabled = false;
            hintText.enabled = false;
            // dont display hint
        }
    }
    public void DisplayHintScreen() {
        if (!Globals.EndingHintChecked.Contains(correspondingEnding))
        {
            EndingHintChecked();
        }
        title.text = "???";
        descriptionText.enabled = false;
        hintText.enabled = true;
        endingInstructionsText.enabled = false;
        hintText.text = endingContent.Hint;

    }
    public void EndingHintChecked() {
        Globals.EndingHintChecked.Add(correspondingEnding);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
