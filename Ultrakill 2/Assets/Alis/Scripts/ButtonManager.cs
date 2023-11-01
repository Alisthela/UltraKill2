using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
