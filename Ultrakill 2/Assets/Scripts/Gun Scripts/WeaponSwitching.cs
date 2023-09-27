using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 1;

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
        Debug.Log(transform.GetChild(0).GetComponent<GunData>().reloading);
        if (Input.GetKeyUp(KeyCode.Alpha1) && (!reloading1 && !reloading2))
            selectedWeapon = 1;
        if (Input.GetKeyUp(KeyCode.Alpha2) && (!reloading0 && !reloading2))
            selectedWeapon = 2;
        if (Input.GetKeyUp(KeyCode.Alpha3) && (!reloading0 && !reloading1))
            selectedWeapon = 3;

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
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
}
