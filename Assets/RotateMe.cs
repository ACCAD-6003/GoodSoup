using UnityEngine;

public class RotateMe : MonoBehaviour
{
    // Rotation speed factor
    public float rotationSpeed = 10f;

    // Random rotation target values
    private Vector3 targetRotation;
    private Vector3 currentRotation;

    void Start()
    {
        // Initialize the target rotation with a random value
        targetRotation = GetRandomRotation();
    }

    void Update()
    {
        // Smoothly interpolate current rotation towards target rotation
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime);

        // Apply the rotation to the object
        transform.Rotate(currentRotation * rotationSpeed * Time.deltaTime);

        // Update target rotation if close to current target
        if (Vector3.Distance(currentRotation, targetRotation) < 0.1f)
        {
            targetRotation = GetRandomRotation();
        }
    }

    // Generate random rotation values for each axis
    private Vector3 GetRandomRotation()
    {
        return new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );
    }
}
