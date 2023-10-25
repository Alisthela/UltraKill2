using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 1;
    public bool switching;
    public GameManager gameManager;

    void Start()
    {
        SelectWeapon(); 
    }

    void Update()
    {
        bool reloading0 = transform.GetChild(0).GetComponent<GunData>().reloading;
        bool reloading1 = transform.GetChild(1).GetComponent<GunData>().reloading;
        bool reloading2 = transform.GetChild(2).GetComponent<GunData>().reloading;
        int previousSelectedWeapon = selectedWeapon;
        //Debug.Log(transform.GetChild(0).GetComponent<GunData>().reloading);
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            selectedWeapon = 1;
            gameManager.m_CurrentGun = CurrentGun.Pistol;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            selectedWeapon = 2;
            gameManager.m_CurrentGun = CurrentGun.Shotgun;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            selectedWeapon = 3;
            gameManager.m_CurrentGun = CurrentGun.Rifle;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            switching = true;
            SelectWeapon();
            Invoke("SwitchOn", 0.5f);
        }
    }

    void SelectWeapon()
    {
        int i = 1;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;

        }
    }

    void SwitchOn()
    {
        switching = false;
    }
}
