using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TestHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public bool oneTime = false;
    public GameObject dataManager;

    private void Update()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            RoundCounter.instance.gameOver = true;
        }
    }
}
