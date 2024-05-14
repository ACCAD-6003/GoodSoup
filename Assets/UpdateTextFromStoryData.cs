using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using static StoryDatastore;

public class UpdateTextFromStoryData : MonoBehaviour
{
    public StoryDataType type;
    public TextMeshProUGUI textMeshPro;
    private void Update()
    {
        textMeshPro.text = $"{Instance.GetStoryDataValue(type)}";
    }
}
