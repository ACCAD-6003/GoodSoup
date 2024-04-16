using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingUIElement : MonoBehaviour
{
    [SerializeField] Ending correspondingEnding;
    [SerializeField] TextMeshProUGUI scoreNumber;
    void OnEnable()
    {
        // change this to actually give a score
        scoreNumber.text = "0";
    }
}
