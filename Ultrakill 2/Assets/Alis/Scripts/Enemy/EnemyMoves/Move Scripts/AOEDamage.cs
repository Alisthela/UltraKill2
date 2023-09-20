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
    public TestHealth Health;

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
            Health = collisionInfo.GetComponent<TestHealth>();
            StartCoroutine(DamageOverTime());
        }
    }

    private IEnumerator DamageOverTime()
    {
        collisionBegin = false;
        Health.playerHealth = Health.playerHealth - (int)skillDamage;
        yield return new WaitForSeconds(1f);
        collisionBegin = true;
    }
}
