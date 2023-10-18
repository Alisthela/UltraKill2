using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : MonoBehaviour
{
    [Header("Shooting")]
    public int damage;
    public float speed;

    [Header("Reloading")]
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public bool automatic;
    [System.NonSerialized] public int currentAmmo;
    [System.NonSerialized] public float secondsSinceFired;
    [System.NonSerialized] public bool reloading;
    [System.NonSerialized] public bool shootDelay;
    [System.NonSerialized] public bool usingAbility;
}
