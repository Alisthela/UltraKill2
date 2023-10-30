using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDecalScript : MonoBehaviour
{

    private Rigidbody rigidbodyDecal;
    private BoxCollider colliderDecal;

    void Start()
    {
        rigidbodyDecal = this.gameObject.GetComponent<Rigidbody>();
        colliderDecal = this.gameObject.GetComponent<BoxCollider>();
        StartCoroutine("StopMovement");
    }

    public IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.3f);

        colliderDecal.isTrigger = true;

        rigidbodyDecal.useGravity = false;
        rigidbodyDecal.constraints = RigidbodyConstraints.FreezePosition;
        rigidbodyDecal.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
