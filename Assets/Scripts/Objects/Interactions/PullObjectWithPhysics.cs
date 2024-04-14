using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObjectWithPhysics : Interaction
{
    public float moveDistance = 1.184389f;
    public float duration = 1f;
    bool _inPhysicsMode = false;
    Rigidbody _rb;
    bool _blown = false;
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }
    public override void DoAction()
    {
        StartCoroutine(nameof(PullOut));
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
    public IEnumerator Blow()
    {
        _blown = true;
        PutInProgress();
        _inPhysicsMode = true;
        float elapsedTime = 0f;
        Vector3 initialVelocity = _rb.velocity;
        float modifier = 100f;
        var blowDuration = 0.6f + (0.5f * interactionId);
        while (elapsedTime < blowDuration)
        {
            // Calculate the current force based on elapsed time
            float t = elapsedTime / blowDuration;
            Vector3 currentForce = Vector3.Lerp(Vector3.zero, new Vector3 (-1f * modifier, 0, -1.5f * modifier), t);

            // Apply the force
            _rb.velocity = initialVelocity + currentForce * Time.deltaTime;

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final force is applied
        //_rb.velocity = initialVelocity + new Vector3(-0.25f * modifier, 0, -1f * modifier);
    }
    public void EnableRigidBody() {
        _rb.isKinematic = false;
        _rb.useGravity = true;
        _inPhysicsMode = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!_inPhysicsMode) {
            return;
        }
        if (collision.collider.gameObject.layer == 8 || collision.collider.gameObject.name.Contains("Book"))
        {
            return;
        }
        StoryDatastore.Instance.AnyBookDropped.Value = true;
        if (_blown) {
            StoryDatastore.Instance.BooksBlown.Value = true;
        }
        SaveData(StoryDatastore.Instance);
        EndAction();
    }

    public override void LoadData(StoryDatastore data)
    {
        Debug.Log("TRYING TO LOAD");
        if (data.BooksDropped.ContainsKey(interactionId)) {
            Debug.Log("SETTING BOOK FROM SAVE");
            _rb = gameObject.GetComponent<Rigidbody>();
            transform.position = data.BooksDropped[interactionId].location;
            transform.rotation = data.BooksDropped[interactionId].rotation;
            Destroy(this);
        }
    }

    public override void SaveData(StoryDatastore data)
    {
        if (_inPhysicsMode) {

            Debug.Log("SAVING BOOK!");
            data.BooksDropped[interactionId] = (transform.position, transform.rotation);
        }
    }
}
