using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public EnemyVariable enemyValues;
    public EnemyMoves enemyMove;
    public float skillDamage;

    public void Start()
    {
        skillDamage = enemyMove.c_skillDamageMultiplier * enemyValues.c_enemyDamage;
        Destroy(this.gameObject, enemyMove.c_skillDuration);
    }
    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.transform.tag == "Player")
        {
            var Health = collisionInfo.GetComponent<TestHealth>();
            Health.playerHealth = Health.playerHealth - (int)skillDamage;
        }
        else if (collisionInfo.transform.tag == "Weapon")
        {
            Destroy(gameObject);
        }
        // the else if is for parry if added into game (dunno yet)

    }
}
