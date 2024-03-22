using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
    [SerializeField]
    Transform lookingAtObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector3 toXZ(Vector3 v)
    {
        return Vector3.Scale(Vector3.one - Vector3.up, v);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardWorldDir = toXZ(transform.rotation * Vector3.forward);
        Vector3 dirToObject = toXZ(lookingAtObject.position - transform.position);

        float angle = Vector3.Angle(forwardWorldDir, dirToObject);

    }
}
