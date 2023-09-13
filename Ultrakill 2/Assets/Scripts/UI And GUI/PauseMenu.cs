using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public PlayerCam playerCam;

    public GameManager gameManager;

    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject KeybindingsMenu;
    [SerializeField] GameObject OBJ_PauseMenu;
    [SerializeField] GameObject BaseButton;

    public TMP_Text Txt_Sensitivty;

    private bool OptionsOpen = false;
    private bool KeybindingsOpen = false;

    public Slider SensitivitySlider;
    public float SensitivityAmount;

    public bool GameBeingplayed = true;

    // Start is called before the first frame update
    void Start()
    {
        OptionsMenu.SetActive(false);
        KeybindingsMenu.SetActive(false);
        OBJ_PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        SensitivityAmount = SensitivitySlider.value;
        playerCam.sensX = SensitivityAmount;
        playerCam.sensY = SensitivityAmount;
        Txt_Sensitivty.text = SensitivityAmount.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameBeingplayed == true)
            {
                ResumeGame();
            }
            else if (GameBeingplayed == false)
            {
                PauseGame();
            }
        }
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        OBJ_PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameBeingplayed = true;
    }
    public void OptionsMenuBuntton()
    {
        if (OptionsOpen == false)
        {
            OptionsMenu.SetActive(true);
            BaseButton.SetActive(false);
            OptionsOpen = true;
        }
        else if (OptionsOpen == true)
        {
            OptionsMenu.SetActive(false);
            BaseButton.SetActive(true);
            OptionsOpen = false;
        }
    }
    public void KeyBindingMenuButton()
    {
        if (KeybindingsOpen == false)
        {
            KeybindingsMenu.SetActive(true);
            KeybindingsOpen = true;
        }
        else if (KeybindingsOpen == true)
        {
            KeybindingsMenu.SetActive(false);
            KeybindingsOpen = false;
        }
    }
    public void quitgame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }

    public void ResumeGame()
    {
        OBJ_PauseMenu.SetActive(false);
        gameManager.m_GameState = GameManager.GameState.Playing;
        GameBeingplayed = false;
    }

    void PauseGame()
    {
        OBJ_PauseMenu.SetActive(true);
        gameManager.m_GameState = GameManager.GameState.Pause;
        GameBeingplayed = true;
    }
}
