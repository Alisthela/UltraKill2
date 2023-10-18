using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    PlayerCam playercam;

    public UpgradeCard upgradeCard;
    public UpgradeCard upgradeCard2;
    public UpgradeCard upgradeCard3;
    public float MoneyAmount = 0f;
    public float curretAmmoAmount;

    public GunData PistolData;
    public GunData ShotGunData;
    public GunData RifleData;

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

    public enum CurrentGun
    {
        Pistol,
        Shotgun,
        Rifle
    }

    public GameState m_GameState;
    public CurrentGun m_CurrentGun;

    public GameState State { get { return m_GameState; } }

    public TMP_Text m_MessageText;

   // public Button m_NewGameButton;


    public TMP_Text Txt_Ammo;

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
                upgradeCard.iscardsative = false;

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
                UpgradeCards.SetActive(true);
                upgradeCard.iscardsative = true;
                upgradeCard2.iscardsative = true;
                upgradeCard3.iscardsative = true;
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

        switch (m_CurrentGun)
        {
            case CurrentGun.Pistol:
                Txt_Ammo.SetText(PistolData.currentAmmo.ToString() + "/" + PistolData.magSize.ToString());
                break;
            case CurrentGun.Shotgun:
                Txt_Ammo.SetText(ShotGunData.currentAmmo.ToString() + "/" + ShotGunData.magSize.ToString());
                break;
            case CurrentGun.Rifle:
                Txt_Ammo.SetText(RifleData.currentAmmo.ToString() + "/" + RifleData.magSize.ToString());
                break;
        }

        /*
         if (playercam.playerhealth <= 0)
        {
            m_GameState = GameState.GameOver;
        }
        */
    }

}