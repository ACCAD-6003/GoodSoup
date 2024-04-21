using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float defaultShakeDuration = 0.5f;
    public float defaultShakeAmount = 0.1f;

    private Vector3 originalPosition;
    private float shakeDuration = 0f;
    private float shakeAmount = 0f;

    void Start()
    {
        // Store the original position of the camera
        originalPosition = transform.position;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            // Generate random offset based on the sine curve
            float offsetX = Mathf.Sin(Time.time * 50) * shakeAmount;
            float offsetY = Mathf.Sin(Time.time * 50) * shakeAmount;

            // Apply the offset to the camera's position
            Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0);
            transform.position = originalPosition + shakeOffset;

            // Reduce the shake duration over time
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            // Smoothly interpolate back to the original position
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * 5);
        }
    }

    // Call this method to trigger the camera shake effect
    public void ShakeCamera(float duration, float amount)
    {
        // If the camera is not already shaking, start the shake effect
        if (shakeDuration <= 0)
        {
            shakeDuration = duration;
            shakeAmount = amount;
        }
        else
        {
            // If the camera is already shaking, optionally accumulate the shake parameters
            // Here, I'm just using the new parameters directly, but you can adjust this logic as needed
            shakeDuration = duration;
            shakeAmount = amount;
        }
    }

    // Call this method to trigger the camera shake effect
    public void ShakeCamera()
    {
        ShakeCamera(defaultShakeDuration, defaultShakeAmount);
    }


    // Method to reset shake values to defaults
    public void ResetShake()
    {
        shakeDuration = defaultShakeDuration;
        shakeAmount = defaultShakeAmount;
    }
}