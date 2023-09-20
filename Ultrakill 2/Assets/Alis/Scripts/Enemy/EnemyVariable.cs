using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Create New Enemy")]
public class EnemyVariable : ScriptableObject
{
    public string enemyID;

    public float c_enemyHealth; // c = current, o = original (non changing)
    public float c_enemyDamage;

    public float o_enemyHealth;
    public float o_enemyDamage;

    public EnemyType enemyType;

    public List<EnemyMoves> enemyMoves = new List<EnemyMoves>();

    public enum EnemyType
    {
        // put enemy types here once names are figured out

        enemyVar1,
        enemyVar2,
        enemyVar3,
        enemyVar4,
        enemyBoss1,
        enemyBoss2,

    }
}
