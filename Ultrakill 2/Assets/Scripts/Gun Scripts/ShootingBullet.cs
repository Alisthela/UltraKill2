using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public GameObject player;
    public float speed = 5f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            ShootBullet();
    }

    private void ShootBullet()
    {
        GameObject cB = Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
        Rigidbody rb = cB.GetComponent<Rigidbody>();
        rb.AddForce(spawnPoint.forward * speed, ForceMode.Impulse);
    }
}
