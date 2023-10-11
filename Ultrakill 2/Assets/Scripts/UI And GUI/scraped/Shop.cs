/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameManager gameManager;

    private bool IsShopOpen = false;

    [SerializeField] GameObject Obj_ShopMenu;
    
    public TMP_Text MoneyText;
    public TMP_Text AmmoText;

    // Start is called before the first frame update
    void Start()
    {
        Obj_ShopMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = "$" + gameManager.MoneyAmount.ToString();
        AmmoText.text = "Ammo:" + gameManager.AmmoAmount.ToString();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsShopOpen == false)
            {
                OpenMenu();
            }
            else if (IsShopOpen == true)
            {
                CloseMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            gameManager.MoneyAmount += 10;
        }
    }

    public void OpenMenu()
    {
        Obj_ShopMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        IsShopOpen = true;
    }

    public void CloseMenu()
    {
        Obj_ShopMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        IsShopOpen = false;
    }
}
*/