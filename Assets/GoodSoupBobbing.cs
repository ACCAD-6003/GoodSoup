using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSoupBobbing : MonoBehaviour
{
    private Vector3 titleSize;
    private Vector3 normalPosition;
    public List<GameObject> buttons;
    public GameObject soup;
    private Vector3 buttonSize;
    void OnEnable()
    {
        if (Globals.FirstTitleScreen)
        {
            normalPosition = transform.position;
            titleSize = transform.localScale;
            transform.position = normalPosition - new Vector3(0, 300, 0);
            transform.localScale = Vector3.zero;
            foreach (var button in buttons)
            {
                buttonSize = button.transform.localScale;
                button.transform.localScale = Vector3.zero;
            }
        }

        if (Globals.FirstTitleScreen)
        {
            StartCoroutine(TitleScreenAnimation());
        }
        else
        {
            transform.position -= new Vector3(0f, -50f, 0f);
            StartCoroutine(BobbingAnimation());
            StartCoroutine(RotationCoroutine());
        }
    }
    private IEnumerator TitleScreenAnimation() { 
        yield return StartCoroutine(EndingSetup.EnlargeAndShrinkTransform(transform, titleSize.x * 1.35f, 1f, titleSize.x, 0.5f));

        float lerpTime = 1.0f;
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < lerpTime)
        {
            transform.position = Vector3.Lerp(startPosition, normalPosition, elapsedTime / lerpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we end up exactly at the normalPosition
        transform.position = normalPosition;

        StartCoroutine(BobbingAnimation());
        StartCoroutine(RotationCoroutine());

        foreach (var button in buttons) {
            yield return StartCoroutine(EndingSetup.EnlargeAndShrinkTransform(button.transform, buttonSize.x * 1.1f, 0.5f, buttonSize.x, 0.25f));
            yield return new WaitForSeconds(0.25f);
        }

    }
    IEnumerator BobbingAnimation()
    {
        Vector3 initialPosition = transform.position;
        float bobRange = 20f;
        float bobSpeed = 3f;

        while (true)
        {
            float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobRange;
            transform.position = initialPosition + new Vector3(0, yOffset, 0);
            yield return null;
        }
    }

    IEnumerator RotationCoroutine()
    {
        while (true)
        {
            // Rotate the object around its X axis
            soup.transform.Rotate(Vector3.right, Time.deltaTime * 10f, Space.Self);
            yield return null;
        }
    }
}
