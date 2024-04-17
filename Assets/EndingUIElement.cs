using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingUIElement : MonoBehaviour
{
    [SerializeField] Ending correspondingEnding;
    [SerializeField] GameObject reveal, hint, unlocked;
    [SerializeField] TextMeshProUGUI scoreNumber;
    void OnEnable()
    {
        if (Globals.UnlockedEndings.Contains(correspondingEnding)) { 
            reveal.SetActive(false);
            hint.SetActive(false);
            unlocked.SetActive(true);
        }
        // change this to actually give a score
        scoreNumber.text = "0";
    }
}
