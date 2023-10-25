using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using TMPro;
using System;

public class TestHealth : MonoBehaviour
{
    public float startHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = startHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
