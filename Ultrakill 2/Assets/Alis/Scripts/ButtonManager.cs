using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject highscoreMenu;
    public GameObject mainMenu;

    public TextMeshProUGUI name;
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;

    public Save dataManager;

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        RoundCounter.instance.gameEnd = false;
        SceneManager.LoadScene(0);
    }

    public void OpenHighscore()
    {
        name.text = "Name: " + dataManager.playerName;
        score.text = "Score: " + dataManager.playerScore.ToString();
        time.text = "Time: " + Mathf.RoundToInt(dataManager.timePlayed) + " seconds";

        mainMenu.SetActive(false);
        highscoreMenu.SetActive(true);
    }

    public void CloseHighscore()
    {
        mainMenu.SetActive(true);
        highscoreMenu.SetActive(false);
    }
}
