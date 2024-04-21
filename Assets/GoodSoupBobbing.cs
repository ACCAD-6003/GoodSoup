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
    private void OnEnable()
    {
        if (initialPosition == Vector3.zero) {
            initialPosition = transform.localPosition;
        }
        if (Globals.FirstTitleScreen)
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
            transform.localPosition = Vector3.Lerp(startPosition, initialPosition, Mathf.Sin((elapsedTime / lerpTime) * Mathf.PI * 0.5f)); // Using sine curve for easing
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we end up exactly at the initial position
        transform.localPosition = initialPosition;

        StartCoroutine(BobbingAnimation());
        StartCoroutine(RotationCoroutine());

        foreach (var button in buttons)
        {
            StartCoroutine(EndingSetup.EnlargeAndShrinkTransform(button.transform, buttonSize.x * 1.1f, 0.5f, buttonSize.x, 0.25f));
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator BobbingAnimation()
    {
        float bobRange = 10f;
        float bobSpeed = 0.75f;

        while (true)
        {
            // Calculate the vertical offset using a sine function
            float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobRange;

            // Apply the offset to the object's position
            transform.localPosition = initialPosition + new Vector3(0, yOffset, 0);

            yield return null;
        }
    }

    IEnumerator RotationCoroutine()
    {
        float initialAngle = -55f; // Initial angle of the object
        float tiltAngle = 60f; // Angle to tilt from the initial position
        float rotationSpeed = 1f; // Speed of rotation during tilt state
        float quickRotationSpeed = -360f; // Speed of rotation during the quick rotation state

        while (true)
        {
            // Tilt state: rotate back and forth twice between two angles
            for (int i = 0; i < 2; i++)
            {
                float startAngle = initialAngle + (i % 2 == 0 ? tiltAngle : -tiltAngle);
                float targetAngle = initialAngle + (i % 2 == 0 ? -tiltAngle : tiltAngle);
                float elapsedTime = 0f;

                while (elapsedTime < Mathf.PI) // Half of a sine wave for one direction
                {
                    float t = Mathf.Sin(elapsedTime) * 0.5f + 0.5f; // Convert sine wave to [0, 1]
                    float angle = Mathf.Lerp(startAngle, targetAngle, t);
                    soup.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
                    elapsedTime += Time.deltaTime * rotationSpeed;
                    yield return null;
                }
            }

            // Quick rotation state: Rotate 360 degrees
            float totalRotation = 0f;
            while (totalRotation > -360f)
            {
                float rotationAmount = quickRotationSpeed * Time.deltaTime;
                soup.transform.Rotate(Vector3.right, rotationAmount, Space.Self);
                totalRotation += rotationAmount;
                yield return null;
            }
        }
    }



}
