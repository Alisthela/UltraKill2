using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    public EnemyVariable enemyValues;
    public EnemyMoves enemyMove;
    public float skillDamage;
    public float timer;
    public bool collisionBegin;
    public PlayerHealth playerHealth;

    public void Start()
    {
        skillDamage = enemyMove.c_skillDamageMultiplier * enemyValues.c_enemyDamage;
        collisionBegin = true;
        Destroy(this.gameObject, enemyMove.c_skillDuration);
    }

    private void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.transform.tag == "Player" && collisionBegin == true)
        {
            playerHealth = collisionInfo.transform.GetComponent<PlayerHealth>();
            StartCoroutine(DamageOverTime());
        }
    }

    private IEnumerator DamageOverTime()
    {
        collisionBegin = false;
        playerHealth.TakeDamage(skillDamage);
        yield return new WaitForSeconds(1f);
        collisionBegin = true;
    }
}
