using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSoundEffectAndDie : MonoBehaviour
{
    [SerializeField] SplineFollower _follower;
    [SerializeField] AudioSource _src;
    [SerializeField] AudioClip _clip;
    private void Start()
    {
        _follower.onNode += OnNodePassed;
    }
    void OnNodePassed(List<SplineTracer.NodeConnection> passed)
    {
        if (passed[0].node.name == "ResetBook") {
            Destroy(_follower.gameObject);
            _src.PlayOneShot(_clip);
        }
    }
}