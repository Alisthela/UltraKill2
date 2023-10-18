using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyVariable;

public class EnemyCommon : MonoBehaviour
{

    public EnemyAttackRange enemyAttackRange;
    public EnemyInfo enemyInfo;
    public List<EnemyMoves> enemyMoves;
    public SphereCollider attackRange;
    public string skillID;

    public GameObject enemySpawnParticle;
    public GameObject enemyBloodParticle;
    public GameObject enemyBloodDecal;

    public bool takeLessDamage;
    public float damageReductionMultiplier;

    public float c_enemyHealth;
    public float c_enemyDamage;

    private void Start()
    {
        EnemySpawn.instance.Enemies.Add(this.gameObject);
        enemyAttackRange = this.GetComponentInChildren<EnemyAttackRange>();
        enemyInfo = this.GetComponent<EnemyInfo>();
        enemyMoves = enemyAttackRange.enemyMoves;

        var spawnParticle = Instantiate(enemySpawnParticle, this.gameObject.transform);
        Destroy(spawnParticle, 0.5f);

        c_enemyHealth = enemyInfo.enemyVariable.c_enemyHealth;
        c_enemyDamage = enemyInfo.enemyVariable.c_enemyDamage;
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "PlayerProjectile")
        {
            var bloodParticle = Instantiate(enemyBloodParticle, this.gameObject.transform);

            Destroy(bloodParticle, 3f);

            if (takeLessDamage == false)
            {
                var projectileInformation = collisionInfo.transform.GetComponent<BulletDataScript>(); // whatever script is in charge of projectile's damage will be put here
                c_enemyHealth -= projectileInformation.projectileDamage; // subtract the projectiles damage from enemy's current health
            }
            else if (takeLessDamage == true)
            {
                var projectileInformation = collisionInfo.transform.GetComponent<BulletDataScript>();
                c_enemyHealth -= projectileInformation.projectileDamage * damageReductionMultiplier;
            }

            if (c_enemyHealth <= 0)
            {
                c_enemyHealth = 0;
                EnemySpawn.instance.Enemies.Remove(this.gameObject);
                ScoreTracker.instance.UpdateScore(enemyInfo);
                Destroy(this.gameObject);
            }            
        }
    }

    // melee damage is taken by meleescript 
}
