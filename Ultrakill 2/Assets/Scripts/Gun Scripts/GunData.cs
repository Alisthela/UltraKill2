using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : MonoBehaviour
{
    [Header("Shooting")]
    public float damage;
    public float speed;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float fireRateTime;
    public float reloadTime;
    public bool reloading;
    public bool automatic;

    [Header("Ability")]
    public float abilityCooldown;
}
