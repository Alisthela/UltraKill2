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
        currentHealthTXT = playerHealth.currentHealth;
        maxHealthTXT = playerHealth.startHealth;
    }

    void Update()
    {
        if (upgradeCards.activeSelf == true)
        {
            healthText.text = "";
        }
        else
        {
            healthText.text = currentHealthTXT.ToString() + "/" + maxHealthTXT.ToString();
        }
    }
}
