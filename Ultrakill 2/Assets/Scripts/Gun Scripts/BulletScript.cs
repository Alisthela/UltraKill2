using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2 );
    }
    private void OnTriggerEnter(Collision collision)
    {
        //if (collision.gameObject.layer == )  Need to know enemies layer
            // Destroy gameobject or run kill function
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
            Destroy(gameObject);
    }
}
