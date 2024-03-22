using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BurnerHeatToText : MonoBehaviour
{
    StoryDatastore data;

    public void Start()
    {
        data = FindObjectOfType<StoryDatastore>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = "Burner Heat: " + data.BurnerHeat.ToString();
    }
}
