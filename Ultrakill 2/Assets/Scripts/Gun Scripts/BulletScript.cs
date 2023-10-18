using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float killTime = 2f;
    public Vector3 initialRotation = new Vector3(0 ,0 ,0);
    void Start()
    {
        Destroy(gameObject, killTime);
        transform.Rotate(initialRotation);
    }
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Wall") || collider.gameObject.layer == LayerMask.NameToLayer("Ground") || collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            Destroy(gameObject);
    }
}
