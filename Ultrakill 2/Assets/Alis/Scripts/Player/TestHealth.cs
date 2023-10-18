using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using TMPro;

public class TestHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public GameObject dataManager;

    private void Update()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            RoundCounter.instance.gameOver = true;
            // probably wont be using this for health, can ignore above
        }
    }
}
