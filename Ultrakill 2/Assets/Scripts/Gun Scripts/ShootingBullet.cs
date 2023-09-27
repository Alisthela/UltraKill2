using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public GameObject shotgunBullet;
    public GameObject player;
    public GunData gunData;

    private void Start()
    {
        gunData.currentAmmo = gunData.magSize;
        gunData.secondsSinceFired = gunData.fireRate;
    }

    private void Update()
    {
        if ((gunData.currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R)) && !gunData.reloading)
        {
            gunData.reloading = true;
            StartCoroutine(Reload());
        }
        else
        {
            if (gunData.secondsSinceFired > 0 && !gunData.shootDelay)
                if (Input.GetKeyDown(KeyCode.Mouse0) && !gunData.reloading)
                {
                    if (bullet == shotgunBullet)
                        ShootShotgunBullet();
                    else
                        ShootBullet();
                    gunData.shootDelay = true;
                    StartCoroutine(ShootDelay());
                }
            if (Input.GetKeyDown(KeyCode.Mouse1) && !gunData.usingAbility)
                UseAbility();
        }
    }

    private void ShootBullet()
    {
        GameObject cB = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = cB.GetComponent<Rigidbody>();
        rb.AddForce(spawnPoint.forward * gunData.speed, ForceMode.Impulse);
        gunData.currentAmmo -= 1;
    }

    private void ShootShotgunBullet()
    {
        //Vector3 randomNumber = Random.Range((-10, 10), (-10, 10), (-10, 10));
        GameObject cB = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = cB.GetComponent<Rigidbody>();
        rb.AddForce(spawnPoint.forward * gunData.speed, ForceMode.Impulse);
        gunData.currentAmmo -= 1;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(gunData.fireRate);
        gunData.secondsSinceFired = gunData.fireRate;
        gunData.shootDelay = false;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private void UseAbility()
    {

    }
}
