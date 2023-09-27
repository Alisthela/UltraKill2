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
    private void OnTiggerEnter(Collider collider)
    {
        //if (collision.gameObject.layer == )  Need to know enemies layer
            // Destroy gameobject or run kill function
        if (collider.gameObject.layer == 7 || collider.gameObject.layer == 8)
            Destroy(gameObject);
    }
}
