using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float startHealth;
    public float currenthealth;


    // Update is called once per frame
    void Update()
    {
        
    }
    void TakeDamage(float damage)
    {
        currenthealth -= damage;
    }
}
