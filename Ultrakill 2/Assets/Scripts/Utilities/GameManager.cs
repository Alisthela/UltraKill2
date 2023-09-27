using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public UpgradeCard upgradeCard;
    public float MoneyAmount = 0f;
    public float AmmoAmount = 0f;

    public float Round = 1f;

    public GameObject UpgradeCards;

    //GameState stuff
    private float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }

    public enum GameState
    {
        Start,
        Playing,
        Pause,
        BetweenRound,
        GameOver
    };

    public GameState m_GameState;

    public GameState State { get { return m_GameState; } }

    public TMP_Text m_MessageText;

   // public Button m_NewGameButton;


    public TMP_Text Txt_Money;

    bool isGameOver = false;


    // Start is called before the first frame update
    private void Awake()
    {
        m_GameState = GameState.Start;
        UpgradeCards.SetActive(false);
    }

    private void Start()
    {
       // m_NewGameButton.gameObject.SetActive(false);
        m_MessageText.gameObject.SetActive(true);
        m_MessageText.text = "Press Enter/Return to start";
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_GameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    m_MessageText.text = "";
                    m_MessageText.gameObject.SetActive(false);
                    m_GameState = GameState.Playing;
                }
                break;

            case GameState.Playing:
                Cursor.lockState = CursorLockMode.Locked;

                if (isGameOver == true)
                {
                    m_GameState = GameState.GameOver;

                    Cursor.lockState = CursorLockMode.None;

                    Time.timeScale = 0f;
                }
                break;

            case GameState.Pause:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;

                break;

            case GameState.BetweenRound:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Round += 1;
                UpgradeCards.SetActive(true);
                upgradeCard.iscardsative = true;
                break;
            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_gameTime = 0;
                    m_GameState = GameState.Playing;
                    m_MessageText.text = "";
                }
                break;

        }

    }

}
