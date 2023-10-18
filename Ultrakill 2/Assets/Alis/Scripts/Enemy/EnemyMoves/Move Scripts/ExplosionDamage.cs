using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public EnemyVariable enemyValues;
    public EnemyMoves enemyMove;
    public bool collisionBegin;
    public float skillDamage;

    public void Start()
    {
        skillDamage = enemyMove.c_skillDamageMultiplier * enemyValues.c_enemyDamage;
        Destroy(this.gameObject, enemyMove.c_skillDuration);
    }
    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.transform.tag == "Player" && collisionBegin == true)
        {
            var Health = collisionInfo.GetComponent<TestHealth>();
            Health.playerHealth = Health.playerHealth - (int)skillDamage;
            collisionBegin = false;
        }
    }
}