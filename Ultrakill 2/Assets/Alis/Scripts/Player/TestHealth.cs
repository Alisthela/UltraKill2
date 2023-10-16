using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using TMPro;

public class TestHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public bool oneTime = false;
    public GameObject dataManager;
    public TextMeshProUGUI playerHealthText;

    private void Update()
    {
        playerHealthText.text = playerHealth.ToString();

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            RoundCounter.instance.gameOver = true;
        }
    }
}
