using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIShowScoreTime : MonoBehaviour
{
    public GameObject upgradeCards;
    public GameObject pauseMenu;
    public ScoreTracker playerScore;
    public RoundCounter roundCounter;
    public float currentScoreTXT;
    public float currentTimeTXT;
    public TextMeshProUGUI scoreTimeText;

    void Start()
    {
        scoreTimeText = this.gameObject.transform.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        currentScoreTXT = playerScore.playerScore;
        currentTimeTXT = roundCounter.timePlayed;
        currentTimeTXT = Mathf.RoundToInt(currentTimeTXT);
        scoreTimeText.text = "Score: " + currentScoreTXT + "\n" + "Time: " + currentTimeTXT;

        if (upgradeCards.activeSelf == true || pauseMenu.activeSelf == true)
        {
            scoreTimeText.text = "";
        }
    }
}
