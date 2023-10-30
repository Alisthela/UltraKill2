using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageOnTouch : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }
}
