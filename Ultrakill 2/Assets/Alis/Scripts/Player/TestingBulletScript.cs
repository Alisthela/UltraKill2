using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBulletScript : MonoBehaviour
{
    public float life;
    public float projectileDamage;

    void Start()
    {
        life = 2f;
        projectileDamage = 10f;
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(life);
        Destroy(this.gameObject);
    }
}
