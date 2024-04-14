using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadiatorTemp : Interaction
{
    [SerializeField] Material radiatorMaterial, towelMaterial;
    void Awake()
    {
        radiatorMaterial.color = new Color(0.76f, 0.76f, 0.76f);
        towelMaterial.color = new Color(0.9764705882352941f, 0.7843137254901961f, 0.8352941176470589f);
    }
    public override void DoAction()
    {
        if (StoryDatastore.Instance.RadiatorHot.Value)
        {
            StartCoroutine(CoolDown());
        }
        else
        {
            StartCoroutine(HeatUp());
        }
        StoryDatastore.Instance.RadiatorHot.Value = !StoryDatastore.Instance.RadiatorHot.Value;
    }
    IEnumerator HeatUp() {
        yield return ChangeColor(radiatorMaterial.color, new Color(0.7764705882352941f, 0.44313725490196076f, 0.44313725490196076f), radiatorMaterial);
        yield return ChangeColor(towelMaterial.color, new Color(0.9176470588235294f, 0.4980392156862745f, 0.6078431372549019f), towelMaterial);
        StoryDatastore.Instance.TowelHot.Value = true;
        EndAction();
    }
    IEnumerator CoolDown()
    {
        yield return ChangeColor(radiatorMaterial.color, new Color(0.76f, 0.76f, 0.76f), radiatorMaterial);
        yield return ChangeColor(towelMaterial.color, new Color(0.9764705882352941f, 0.7843137254901961f, 0.8352941176470589f), towelMaterial);
        StoryDatastore.Instance.TowelHot.Value = false;
        EndAction();
    }

    IEnumerator ChangeColor(Color from, Color to, Material mat)
    {
        float duration = 1f; // Duration of the color change
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mat.color = Color.Lerp(from, to, elapsedTime / duration);
            yield return null;
        }
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
