using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckedUpLightCone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    int _coneHitLayer = 29;

    // Update is called once per frame
    void Update()
    {
        bool didHit = Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, 999, 1 << _coneHitLayer);

        if (!didHit)
        {
            return;
        }

        float dist = (hit.point - transform.position).magnitude;

        transform.localScale = 0.0265f * dist * Vector3.one;
    }
}
