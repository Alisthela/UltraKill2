using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;

    public List<Transform> Spawnpoints = new List<Transform>();
    public List<GameObject> Enemies = new List<GameObject>();

    public GameObject enemy1; // ground enemy variation
    public GameObject enemy2; // flying variation
    public GameObject enemyboss1; // big boss variation

    // until models are made keep the above variations    

    public List<EnemyVariable> EnemyTypes = new List<EnemyVariable>();
    public List<EnemyVariable> BossEnemyTypes = new List<EnemyVariable>();

    public int randomCommonEnemy;
    public int randomBossEnemy;
    public int spawnAmount;
    public int spawnNumber;

    public float commonSpawnAmount;
    public float bossSpawnAmount;

    public float spawnTimer;
    public float o_spawnTimer = 0.8f;

    public float mathVarSpawn;

    public float c_commonEnemyRatio;
    public float c_bossEnemyRatio;
    public float o_commonEnemyRatio = 0.9f;
    public float o_bossEnemyRatio = 0.1f;

    public float difficultyMultiplier;

    public Transform spawnPosition;

    public GameObject longAttackRange; // e.g: guns
    public GameObject meleeAttackRange; // e.g: swords
    public GameObject mediumAttackRange; // e.g: spears
    public GameObject bossMeleeAttackRange; // for melee boss since the scale of parent object not affecting the sphere collider

    public GameObject newEnemy;
    private void Start()
    {
        instance = this;        
    }

    public void UpdateDifficultyValues(float roundNumber)
    {
        mathVarSpawn = roundNumber / 33f;
        spawnTimer = o_spawnTimer - mathVarSpawn;

        if (spawnTimer < 0.1f)
        {
            spawnTimer = 0.1f;
        }

        if (roundNumber < 5)
        {
            c_commonEnemyRatio = o_commonEnemyRatio;
            c_bossEnemyRatio = o_bossEnemyRatio;
            difficultyMultiplier = 1f;
        }
        else if (roundNumber > 5 && roundNumber < 10)
        {
            c_commonEnemyRatio = 0.8f;
            c_bossEnemyRatio = 0.2f;
            difficultyMultiplier = 1.2f;
        }
        else if (roundNumber > 10 && roundNumber < 15)
        {
            c_commonEnemyRatio = 0.7f;
            c_bossEnemyRatio = 0.3f;
            difficultyMultiplier = 1.4f;
        }
        else if (roundNumber > 15 && roundNumber < 20)
        {
            c_commonEnemyRatio = 0.6f;
            c_bossEnemyRatio = 0.4f;
            difficultyMultiplier = 1.6f;
        }
        else if (roundNumber > 20 && roundNumber < 25)
        {
            c_commonEnemyRatio = 0.5f;
            c_bossEnemyRatio = 0.5f;
            difficultyMultiplier = 1.8f;
        }
        else if (roundNumber > 25)
        {
            c_commonEnemyRatio = 0.1f;
            c_bossEnemyRatio = 0.9f;
            difficultyMultiplier = 2f;
        }

        // slowly scales difficulty and ratio of boss to normal enemies (need to adjust this with play testing, + make it infinite)
    }

    public IEnumerator SpawnEnemies(int roundNumber)
    {
        spawnAmount += roundNumber * 2;
        commonSpawnAmount = spawnAmount * c_commonEnemyRatio;
        bossSpawnAmount = spawnAmount * c_bossEnemyRatio;

        commonSpawnAmount = Mathf.Floor(commonSpawnAmount); // round down
        bossSpawnAmount = Mathf.Ceil(bossSpawnAmount); // round up

        yield return new WaitForSeconds(spawnTimer);

        for (int i = 0; i < commonSpawnAmount; i++) // this is common enemy spawner
        {
            spawnNumber = Random.Range(0, Spawnpoints.Count);
            spawnPosition = Spawnpoints[spawnNumber]; // selects random spawn point from list

            randomCommonEnemy = Random.Range(0, EnemyTypes.Count); // selects random enemy type

            yield return new WaitForSeconds(spawnTimer); // delay

            if (EnemyTypes[randomCommonEnemy].enemyType == EnemyVariable.EnemyType.enemyVar4)
            {
                newEnemy = Instantiate(enemy2, spawnPosition.position, spawnPosition.rotation);
                newEnemy.transform.parent = GameObject.Find("Enemies").transform; // spawns in enemy and puts it under enemies gameobject
            }
            else
            {
                newEnemy = Instantiate(enemy1, spawnPosition.position, spawnPosition.rotation);
                newEnemy.transform.parent = GameObject.Find("Enemies").transform;
            }

            var enemyType = newEnemy.GetComponent<EnemyInfo>();
            enemyType.enemyVariable = EnemyTypes[randomCommonEnemy]; // getting enemy info component and assigning the enemy a random type (ranged, melee, magic etc.)

            enemyType.enemyVariable.c_enemyHealth = enemyType.enemyVariable.o_enemyHealth;
            enemyType.enemyVariable.c_enemyDamage = enemyType.enemyVariable.o_enemyDamage; // setting current health and dmg to original health and dmg
            enemyType.enemyVariable.c_enemyHealth = enemyType.enemyVariable.c_enemyHealth * difficultyMultiplier;
            enemyType.enemyVariable.c_enemyDamage = enemyType.enemyVariable.c_enemyDamage * difficultyMultiplier; // multiplying by difficulty multiplier

            foreach (EnemyMoves enemyMove in enemyType.enemyVariable.enemyMoves)
            {
                enemyMove.c_skillAOE = enemyMove.o_skillAOE;
                enemyMove.c_skillDamageMultiplier = enemyMove.o_skillDamageMultiplier;
                enemyMove.c_skillCooldown = 0;
                enemyMove.c_skillDamageReductionMultiplier = enemyMove.o_skillDamageReductionMultiplier;
                enemyMove.c_skillSpeed = enemyMove.o_skillSpeed;
                enemyMove.c_skillDuration = enemyMove.o_skillDuration;
            }

            // for attack ranges, add gameobject with range collider 
            if (enemyType.enemyVariable.enemyType == EnemyVariable.EnemyType.enemyVar1) // basic lunge attack melee
            {
                var Collider = Instantiate(meleeAttackRange, spawnPosition.position, spawnPosition.rotation);
                Collider.transform.parent = newEnemy.transform;
            }
            else if (enemyType.enemyVariable.enemyType == EnemyVariable.EnemyType.enemyVar2) // punch and kick melee
            {
                var Collider = Instantiate(meleeAttackRange, spawnPosition.position, spawnPosition.rotation);
                Collider.transform.parent = newEnemy.transform;
            }
            else if (enemyType.enemyVariable.enemyType == EnemyVariable.EnemyType.enemyVar3) // flesh chunk thrower
            {
                var Collider = Instantiate(longAttackRange, spawnPosition.position, spawnPosition.rotation);
                Collider.transform.parent = newEnemy.transform;
            }
            else if (enemyType.enemyVariable.enemyType == EnemyVariable.EnemyType.enemyVar4) // mage (flying enemy variation)
            {
                var Collider = Instantiate(longAttackRange, spawnPosition.position, spawnPosition.rotation);
                Collider.transform.parent = newEnemy.transform;
            }

            newEnemy = null;
            // Testing purposes below
            // Debug.Log("Spawned in " + enemyType.enemyVariable.enemyType + ", with health " + enemyType.enemyVariable.c_enemyHealth + " and damage " + enemyType.enemyVariable.c_enemyDamage);
        }

        for (int i = 0; i < bossSpawnAmount; i++) // this is boss enemy spawner
        {
            spawnNumber = Random.Range(0, Spawnpoints.Count);
            spawnPosition = Spawnpoints[spawnNumber];

            randomBossEnemy = Random.Range(0, BossEnemyTypes.Count);

            yield return new WaitForSeconds(spawnTimer);

            var newEnemy = Instantiate(enemyboss1, spawnPosition.position, spawnPosition.rotation);
            newEnemy.transform.parent = GameObject.Find("Enemies").transform;

            var enemyType = newEnemy.GetComponent<EnemyInfo>();
            enemyType.enemyVariable = BossEnemyTypes[randomBossEnemy];

            enemyType.enemyVariable.c_enemyHealth = enemyType.enemyVariable.o_enemyHealth;
            enemyType.enemyVariable.c_enemyDamage = enemyType.enemyVariable.o_enemyDamage;
            enemyType.enemyVariable.c_enemyHealth = enemyType.enemyVariable.c_enemyHealth * difficultyMultiplier;
            enemyType.enemyVariable.c_enemyDamage = enemyType.enemyVariable.c_enemyDamage * difficultyMultiplier;

            foreach (EnemyMoves enemyMove in enemyType.enemyVariable.enemyMoves)
            {
                enemyMove.c_skillAOE = enemyMove.o_skillAOE;
                enemyMove.c_skillDamageMultiplier = enemyMove.o_skillDamageMultiplier;
                enemyMove.c_skillCooldown = 0;
                enemyMove.c_skillDamageReductionMultiplier = enemyMove.o_skillDamageReductionMultiplier;
                enemyMove.c_skillSpeed = enemyMove.o_skillSpeed;
                enemyMove.c_skillDuration = enemyMove.o_skillDuration;
            }

            if (enemyType.enemyVariable.enemyType == EnemyVariable.EnemyType.enemyBoss1)
            {
                var Collider = Instantiate(mediumAttackRange, spawnPosition.position, spawnPosition.rotation);
                Collider.transform.parent = newEnemy.transform;
            }
            else if (enemyType.enemyVariable.enemyType == EnemyVariable.EnemyType.enemyBoss2)
            {
                var Collider = Instantiate(bossMeleeAttackRange, spawnPosition.position, spawnPosition.rotation);
                Collider.transform.parent = newEnemy.transform;
            }

            newEnemy.name = "Boss";

            // Testing purposes below
            // Debug.Log("Spawned in " + enemyType.enemyVariable.enemyType + ", with health " + enemyType.enemyVariable.c_enemyHealth + " and damage " + enemyType.enemyVariable.c_enemyDamage);
        }

        RoundCounter.instance.oneTime = false;

        yield return new();
    }

    // when SpawnEnemies is called, enemies will be spawned at random spawnpoints, and spawn amount will increase as game goes on
    // enemies won't be spawned all at once
    // ratio is 10% boss, 90% common at start
    // ratio steadily changes as game progresses
}
