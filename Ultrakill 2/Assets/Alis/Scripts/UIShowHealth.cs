using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIShowHealth : MonoBehaviour
{
    public GameObject upgradeCards;
    public PlayerHealth playerHealth;
    public float currentHealthTXT;
    public float maxHealthTXT;
    public TextMeshProUGUI healthText;

    void Start()
    {
        healthText = this.gameObject.transform.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        currentHealthTXT = playerHealth.currentHealth;
        maxHealthTXT = playerHealth.startHealth;
        healthText.text = "Health: " + currentHealthTXT.ToString() + "/" + maxHealthTXT.ToString();
    }
}
