using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject KeybindingsMenu;

    public TMP_Text Txt_Sensitivty;

    private bool OptionsOpen = false;
    private bool KeybindingsOpen = false;

    public Slider SensitivitySlider;
    public float SensitivityAmount;

    // Start is called before the first frame update
    void Start()
    {
        OptionsMenu.SetActive(false);
        KeybindingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SensitivityAmount = SensitivitySlider.value;
        Txt_Sensitivty.text = SensitivityAmount.ToString(); 
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsMenuBuntton()
    {
        if (OptionsOpen == false)
        {
            OptionsMenu.SetActive(true);
            OptionsOpen = true;
        }
        else if (OptionsOpen == true)
        {
            OptionsMenu.SetActive(false);
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
}
