using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadiatorTemp : Interaction
{
    [SerializeField] Material radiatorMaterial;
    void Awake()
    {
        radiatorMaterial.color = new Color(0.76f, 0.76f, 0.76f);
    }
    public override void DoAction()
    {
        if (StoryDatastore.Instance.RadiatorHot.Value)
        {
            StartCoroutine(ChangeColor(radiatorMaterial.color, new Color(0.76f, 0.76f, 0.76f))); // Hex: C3C3C3
        }
        else
        {
            StartCoroutine(ChangeColor(radiatorMaterial.color, new Color(0.7764705882352941f, 0.44313725490196076f, 0.44313725490196076f))); // Hex: C67171
        }
        StoryDatastore.Instance.RadiatorHot.Value = !StoryDatastore.Instance.RadiatorHot.Value;
    }

    IEnumerator ChangeColor(Color from, Color to)
    {
        float duration = 1f; // Duration of the color change
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            radiatorMaterial.color = Color.Lerp(from, to, elapsedTime / duration);
            yield return null;
        }
        EndAction();
    }
    public override void LoadData(StoryDatastore data)
    {
        // doesnt save yet
    }

    public override void SaveData(StoryDatastore data)
    {
        // doesnt save yet
    }
}
