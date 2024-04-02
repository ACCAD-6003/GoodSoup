using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObjectWithPhysics : Interaction
{
    public float moveDistance = 1.184389f;
    public float duration = 1f;
    bool _inPhysicsMode = false;
    override protected void DoAction()
    {
        StartCoroutine(nameof(PullOut));
    }
    IEnumerator PullOut()
    {
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
    public void EnableRigidBody() {

        var rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        _inPhysicsMode = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!_inPhysicsMode) {
            return;
        }
        StoryDatastore.Instance.AnyBookDropped.Value = true;
        SaveData(StoryDatastore.Instance);
        EndAction();
    }

    public override void LoadData(StoryDatastore data)
    {
        Debug.Log("TRYING TO LOAD");
        if (data.BooksDropped.ContainsKey(interactionId)) {
            Debug.Log("SETTING BOOK FROM SAVE");
            var rb = transform.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
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
