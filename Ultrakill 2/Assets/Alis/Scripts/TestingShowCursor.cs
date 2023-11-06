using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingShowCursor : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject upgradeCards;
    public GameObject gameOver;

    void Update()
    {
        if (pauseMenu.activeSelf == true || upgradeCards.activeSelf == true || gameOver.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
        }
    }
}
