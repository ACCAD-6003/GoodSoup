using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSoupBobbing : MonoBehaviour
{
    private Vector3 titleSize;
    public List<GameObject> buttons;
    public GameObject soup;
    private Vector3 buttonSize;
    private static Vector3 initialPosition = Vector3.zero;
    public EndingUnlocked unlocked;
    private void OnEnable()
    {
        if (initialPosition == Vector3.zero) {
            initialPosition = transform.localPosition;
        }
        if (unlocked.firstTime)
        {
            titleSize = transform.localScale;

            transform.localPosition -= new Vector3(0, 150, 0);
            transform.localScale = Vector3.zero;
            foreach (var button in buttons)
            {
                buttonSize = button.transform.localScale;
                button.transform.localScale = Vector3.zero;
            }
            StartCoroutine(TitleScreenAnimation());
        }
        else
        {
            transform.localPosition -= new Vector3(0f, -50f, 0f);
            StartCoroutine(BobbingAnimation());
            StartCoroutine(RotationCoroutine());
        }
    }

    private IEnumerator TitleScreenAnimation()
    {
        yield return StartCoroutine(EndingSetup.EnlargeAndShrinkTransform(transform, titleSize.x * 1.35f, 1f, titleSize.x, 0.5f));

        float lerpTime = 1.0f;
        float elapsedTime = 0.0f;
        Vector3 startPosition = transform.localPosition;

        while (elapsedTime < lerpTime)
        {
            // Lerp from the lowered position back to the initial position
            transform.localPosition = Vector3.Lerp(startPosition, initialPosition, elapsedTime / lerpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we end up exactly at the initial position
        transform.localPosition = initialPosition;

        StartCoroutine(BobbingAnimation());
        StartCoroutine(RotationCoroutine());

        foreach (var button in buttons)
        {
            yield return StartCoroutine(EndingSetup.EnlargeAndShrinkTransform(button.transform, buttonSize.x * 1.1f, 0.5f, buttonSize.x, 0.25f));
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator BobbingAnimation()
    {
        float bobRange = 20f;
        float bobSpeed = 3f;

        while (true)
        {
            float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobRange;
            transform.localPosition = initialPosition + new Vector3(0, yOffset, 0);
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
