using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PullObjectWithPhysics : Interaction
{
    bool _inPhysicsMode = false;
    Animator _anim;
    override protected void DoAction()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("Pull", true);
    }
    public void EnableRigidBody() {
        Destroy(_anim);
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
        EndAction();
    }
}
