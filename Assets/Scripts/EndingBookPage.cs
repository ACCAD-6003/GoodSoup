using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingBookPage : MonoBehaviour
{
    public Ending correspondingEnding;
    public Image endingImage;
    public TextMeshProUGUI title, descriptionText, hintText, endingInstructionsText;
    public GameObject RevealHint, Hint, Unlocked;
    public List<Star> stars;
    void Awake()
    {
        if (Globals.UnlockedEndings.ContainsKey(correspondingEnding))
        {
            // enable ending screen
        }
        else if (Globals.EndingHintChecked.Contains(correspondingEnding))
        {
            // display hint
        }
        else
        { 
            // dont display hint
        }
    }
    public void EndingHintChecked() {
        Globals.EndingHintChecked.Add(correspondingEnding);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
