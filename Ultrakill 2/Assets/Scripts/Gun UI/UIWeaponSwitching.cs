using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static GameManager;

public class UIWeaponSwitching : MonoBehaviour
{
    public GameManager gm;
    public GameObject pistolOn;
    public GameObject pistolOff;
    public GameObject shotgunOn;
    public GameObject shotgunOff;
    public GameObject AROn;
    public GameObject AROff;

    public Transform pistol;
    public Transform shotgun;
    public Transform AR;

    private void Update()
    {
        if (gm.m_GameState == GameState.Playing)
        {
            if (pistol.gameObject.activeSelf == true)
            {
                pistolOn.SetActive(true);
                shotgunOff.SetActive(true);
                AROff.SetActive(true);
                pistolOff.SetActive(false);
                shotgunOn.SetActive(false);
                AROn.SetActive(false);
            }
            if (shotgun.gameObject.activeSelf == true)
            {
                pistolOff.SetActive(true);
                shotgunOn.SetActive(true);
                AROff.SetActive(true);
                pistolOn.SetActive(false);
                shotgunOff.SetActive(false);
                AROn.SetActive(false);
            }
            if (AR.gameObject.activeSelf == true)
            {
                pistolOff.SetActive(true);
                shotgunOff.SetActive(true);
                AROn.SetActive(true);
                pistolOn.SetActive(false);
                shotgunOn.SetActive(false);
                AROff.SetActive(false);
            }
        }
        else
        {
            pistolOn.SetActive(false);
            pistolOff.SetActive(false);
            shotgunOn.SetActive(false);
            shotgunOff.SetActive(false);
            AROff.SetActive(false);
            AROn.SetActive(false);
        }
    }
}