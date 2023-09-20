using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCooldown : MonoBehaviour
{
    private float time;
    public Transform Enemies;
    public Transform Player; // debugging, delete later

    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Enemies.transform.childCount != 0)
        {
            foreach (Transform Enemy in Enemies)
            {
                var enemyAttackRange = Enemy.GetComponentInChildren<EnemyAttackRange>();
                var enemyCommon = Enemy.GetComponent<EnemyCommon>();
                var enemyMoves = enemyAttackRange.enemyMoves;

                time += Time.deltaTime;
               
                while (time >= 1f)
                {
                    foreach (EnemyMoves enemyMove in enemyMoves)
                    {
                        if (enemyMove.c_skillCooldown > 0)
                        {
                            // below is for debugging delete later when not needed
                            // Debug.Log(enemyMove.c_skillCooldown + " second(s) left until " + enemyMove.skillName + " is off cooldown.");

                            enemyMove.c_skillCooldown -= 1f;

                            if (enemyMove.c_skillCooldown < 0)
                            {
                                enemyMove.c_skillCooldown = 0;
                            }
                        }
                    }
                    time -= 1f;
                }
            }
        }
    }
}
