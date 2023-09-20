using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public GameObject player;
    public GunData gunData;

    private void Update()
    {
        if (!gunData.reloading)
        {
            if (gunData.currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
            {
                gunData.reloading = true;
                StartCoroutine(Reload());
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (gunData.fireRateTime == gunData.fireRate)
                {
                    ShootBullet();
                    gunData.fireRateTime = 0;
                }
                else
                    StartCoroutine(ShootingDelay());

            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
                UseAbility();
        }
    }

    private void ShootBullet()
    {
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, player.transform.rotation.z);
        GameObject cB = Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
        Rigidbody rb = cB.GetComponent<Rigidbody>();
        rb.AddForce(spawnPoint.forward * gunData.speed, ForceMode.Impulse);
        gunData.currentAmmo -= 1;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(gunData.fireRate);
        gunData.fireRateTime = gunData.fireRate;
    }

    private void UseAbility()
    {
        Debug.Log("Used Ability");
    }
}
