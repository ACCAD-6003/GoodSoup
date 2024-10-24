using System.Collections;
using UnityEngine;

public class PullObjectWithPhysics : Interaction
{
    [Header("Pull Settings")]
    [Tooltip("These are modifiers for the book pull out sequence, not the blow settings.")]
    public float moveDistance = 1.184389f;
    public float duration = 1f;
    bool _inPhysicsMode = false;
    Rigidbody _rb;
    bool _blowing = false, _blown = false;
    bool _collided = false;
    [SerializeField] Interaction interactionToFinishOnceCollisionHits;
    [SerializeField] AudioSource _playSoundOnImpact;
    [Header("Blow Settings")]
    [Tooltip("These are modifiers for the book being blown.")]
    [SerializeField] float modifier = 10f;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    public override void DoAction()
    {
        StartCoroutine(nameof(PullOut));
    }
    public void Blow()
    {
        _blowing = true;
        _blown = true;
        StartCoroutine(StopBlowingAfterDuration(duration));
    }

    IEnumerator StopBlowingAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        _blowing = false;
    }

    private void FixedUpdate()
    {
        if (_blowing)
        {
            ApplyBlowForce();
        }
    }

    IEnumerator PullOut()
    {
        _rb.isKinematic = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(moveDistance, 0f, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        EnableRigidBody();
    }

    private void ApplyBlowForce()
    {
        _inPhysicsMode = true;
        var blowDuration = 0.6f + (0.7f * interactionId);

        // Calculate the current force based on elapsed time
        float t = Time.fixedDeltaTime / blowDuration;
        Vector3 currentForce = Vector3.Lerp(Vector3.zero, new Vector3(-2f * modifier, 0, -1.5f * modifier), t);

        // Apply the force
        _rb.AddForce(currentForce, ForceMode.VelocityChange);
    }

    public void EnableRigidBody()
    {
        _rb.isKinematic = false;
        _rb.useGravity = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.sleepThreshold = 0;
        _inPhysicsMode = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!_inPhysicsMode || _collided)
        {
            return;
        }
        if (collision.collider.gameObject.layer == 8 || collision.collider.gameObject.name.Contains("Book"))
        {
            return;
        }
        _collided = true;
        FindObjectOfType<CameraShake>().ShakeCamera();
        StoryDatastore.Instance.AnyBookDropped.Value = true;
        if (_blown)
        {
            Debug.Log("Blown book has collided");
            StoryDatastore.Instance.BooksBlown.Value = true;
            if (StoryDatastore.Instance.CurrentGamePhase.Value == GamePhase.TUTORIAL_BEDROOM && !StoryDatastore.Instance.AmberOutOfBed.Value)
            {
                Debug.Log("Blown book has caused the annoyance to increase.");
                StoryDatastore.Instance.Annoyance.Value += 0.5f;
            }
        }
        interactionToFinishOnceCollisionHits.EndAction();
        _playSoundOnImpact.Play();
        SaveData(StoryDatastore.Instance);
        EndAction();
    }

    public override void LoadData(StoryDatastore data)
    {
        if (data.BooksDropped.ContainsKey(interactionId))
        {
            _rb = gameObject.GetComponent<Rigidbody>();
            transform.position = data.BooksDropped[interactionId].location;
            transform.rotation = data.BooksDropped[interactionId].rotation;
            Destroy(this);
        }
    }

    public override void SaveData(StoryDatastore data)
    {
        if (_inPhysicsMode)
        {
            data.BooksDropped[interactionId] = (transform.position, transform.rotation);
        }
    }
}
