using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealth : MonoBehaviour
{

    // this is just a temporary script until blood healing is finished

    public int playerHealth = 100;

    private void Update()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Destroy(gameObject);

            Debug.Log("Game over");
        }
    }
}
