using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    public static ScoreTracker instance;
    public int playerScore;

    void Start()
    {
        instance = this;
    }

    public void UpdateScore(EnemyInfo enemyInfo)
    {
        switch (enemyInfo.enemyVariable.enemyType)
        {
            case EnemyVariable.EnemyType.enemyVar1:
                {
                    playerScore += 10;
                    break;
                }

            case EnemyVariable.EnemyType.enemyVar2:
                {
                    playerScore += 10;
                    break;
                }

            case EnemyVariable.EnemyType.enemyVar3:
                {
                    playerScore += 15;
                    break;
                }

            case EnemyVariable.EnemyType.enemyVar4:
                {
                    playerScore += 15;
                    break;
                }

            case EnemyVariable.EnemyType.enemyBoss1:
                {
                    playerScore += 50;
                    break;
                }

            case EnemyVariable.EnemyType.enemyBoss2:
                {
                    playerScore += 50;
                    break;
                }
        }

        Debug.Log("Player score is: " + playerScore); // can link this to UI later
    }
}
