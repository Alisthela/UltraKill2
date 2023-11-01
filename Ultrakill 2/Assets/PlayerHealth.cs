using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float startHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = startHealth;
    }

    private void Update()
    {
        Mathf.RoundToInt(currentHealth);
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }
    public void AddHealth(float health)
    {
        currentHealth += health;

        if (currentHealth > startHealth)
        {
            currentHealth = startHealth;
        }
    }
}
