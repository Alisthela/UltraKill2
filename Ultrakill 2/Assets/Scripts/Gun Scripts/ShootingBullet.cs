using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootingBullet : MonoBehaviour
{
    public GameManager gameManager;

    public Transform spawnPoint;
    public GameObject bullet;
    public GameObject shotgunBullet;
    public GameObject player;
    public GunData gunData;
    public GameObject weaponHolder;
    private WeaponSwitching weaponSwitching;

    private void Start()
    {
        weaponSwitching = weaponHolder.GetComponent<WeaponSwitching>();
        gunData.currentAmmo = gunData.magSize;
        gunData.secondsSinceFired = gunData.fireRate;
    }

    private void Update()
    {
        // Switch
        if (weaponSwitching.switching)
        {
            StopCoroutine(Reload());
            gunData.reloading = false;
        }
        else
        {
            // Reload
            if ((gunData.currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R)) && !gunData.reloading)
            {
                gunData.reloading = true;
                gameManager.ReloadingText.SetActive(true);
                StartCoroutine(Reload());
                Debug.Log(gunData.reloading);
            }
            // Shooting
            else
            {
                // Normal
                if (gunData.secondsSinceFired > 0 && !gunData.shootDelay)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && !gunData.reloading && !gunData.automatic)
                    {
                        if (bullet == shotgunBullet)
                            ShootShotgunBullet();
                        else
                            ShootBullet();
                        gunData.shootDelay = true;
                        StartCoroutine(ShootDelay());
                    }
                }
                // Automatic
                if (Input.GetButton("Fire1") && !gunData.reloading && gunData.automatic && gunData.secondsSinceFired > 0 && !gunData.shootDelay)
                {
                    if (bullet == shotgunBullet)
                        ShootShotgunBullet();
                    else
                        ShootBullet();
                    gunData.shootDelay = true;
                    StartCoroutine(ShootDelay());
                }
                // Abilities
                if (Input.GetKeyDown(KeyCode.Mouse1) && !gunData.usingAbility)
                    UseAbility();
            }
        }
    }

    private void ShootBullet()
    {
        GameObject cB = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = cB.GetComponent<Rigidbody>();
        rb.AddForce(spawnPoint.forward * gunData.speed, ForceMode.Impulse);

        var bulletData = cB.GetComponent<BulletDataScript>();
        bulletData.projectileDamage = gunData.damage;

        gunData.currentAmmo -= 1;
    }

    private void ShootShotgunBullet()
    {
        for(int i = 0; i < 97; i++)
        {
            Vector3 position = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
            Quaternion rotation = new Quaternion(spawnPoint.rotation.x+Random.Range(-0.1f, 0.1f), spawnPoint.rotation.y+Random.Range(-0.1f, 0.1f), spawnPoint.rotation.z+Random.Range(-0.1f, 0.1f), 0+spawnPoint.rotation.w);
            GameObject cB = Instantiate(bullet, spawnPoint.position + position, rotation);
            Rigidbody rb = cB.GetComponent<Rigidbody>();           
            rb.AddForce(cB.transform.forward * gunData.speed, ForceMode.Impulse);

            var bulletData = cB.GetComponent<BulletDataScript>();
            bulletData.projectileDamage = gunData.damage;
        }
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
        gameManager.ReloadingText.SetActive(false);
        gunData.reloading = false;
        Debug.Log(gunData.reloading);
    }

    private void UseAbility()
    {

    }
}
