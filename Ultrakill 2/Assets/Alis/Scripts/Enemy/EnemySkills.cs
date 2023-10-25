using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemySkills : MonoBehaviour
{
    public static EnemySkills instance;

    public GameObject player;
    public GameObject enemyAbilities;

    public GameObject bloodPoolObject;

    public GameObject bloodExplosionObject;

    public GameObject warningObject;

    public GameObject launchPoint;
    public GameObject fleshChunk;

    public GameObject spear;
    public GameObject spearSweepIndicator;

    public float originalSpeed = 10f; // change this value when enemy speed gets changed
    public float attackInterval = 0.2f;

    private void Start()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
    }

    public IEnumerator bloodExplosion(EnemyMoves enemyMove, EnemyInfo enemyInfo)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        
        var Scale = new Vector3(enemyMove.c_skillAOE, 0.01f, enemyMove.c_skillAOE);

        var playerFeet = new Vector3(player.transform.position.x, player.transform.position.y -1f, player.transform.position.z);

        var warning = Instantiate(warningObject, playerFeet, player.transform.rotation);
        warning.transform.localScale = Scale;
        warning.transform.parent = enemyAbilities.transform;

        var skillPosition = warning.transform;
        yield return new WaitForSeconds(1f);
        Destroy(warning);

        // play animation here

        var bloodExplosion = Instantiate(bloodExplosionObject, skillPosition.position, skillPosition.rotation);
        bloodExplosion.transform.parent = enemyAbilities.transform;

        var explosionValues = bloodExplosion.GetComponent<ExplosionDamage>();
        explosionValues.enemyMove = enemyMove;
        explosionValues.enemyValues = enemyInfo.enemyVariable;
        explosionValues.collisionBegin = true;

        yield return new WaitForSeconds(enemyMove.c_skillDuration);
        Destroy(bloodExplosion);

        // Debug.Log("used bloodexplosion");
        yield return null;
    }
    public IEnumerator bloodPool(EnemyMoves enemyMove, EnemyInfo enemyInfo)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;

        var Scale = new Vector3(enemyMove.c_skillAOE, 0.01f, enemyMove.c_skillAOE);

        var playerFeet = new Vector3(player.transform.position.x, 0, player.transform.position.z);

        var warning = Instantiate(warningObject, playerFeet, player.transform.rotation);
        warning.transform.localScale = Scale;
        warning.transform.parent = enemyAbilities.transform;

        yield return new WaitForSeconds(1f);
        Destroy(warning);

        // play animation here

        var bloodPool = Instantiate(bloodPoolObject, playerFeet, player.transform.rotation);
        bloodPool.transform.parent = enemyAbilities.transform;

        var bloodPoolValues = bloodPool.GetComponent<AOEDamage>();
        bloodPoolValues.enemyMove = enemyMove;
        bloodPoolValues.enemyValues = enemyInfo.enemyVariable;
        bloodPoolValues.collisionBegin = true;

        yield return new WaitForSeconds(enemyMove.c_skillDuration);
        Destroy(bloodPool);

        // Debug.Log("used bloodpool");
        yield return null;
    }

    public IEnumerator dashTowardsPlayer(EnemyMoves enemyMove, GameObject enemy)
    {        
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;

        var enemySpeed = enemy.GetComponent<NavMeshAgent>();

        enemySpeed.speed = enemyMove.c_skillSpeed;

        // play animation here

        yield return new WaitForSeconds(5f);

        enemySpeed.speed = originalSpeed;

        // Debug.Log("used dashtowardsplayer");
        yield return null;
    }

    public IEnumerator fleshChunkThrow(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;

        var gravity = 200f; // increase gravity to increase projectile speed
        var projectileDistance = Vector3.Distance(enemy.transform.position, player.transform.position);
        var projectileVelocity = projectileDistance / (Mathf.Sin(2 * 20f * Mathf.Deg2Rad) / gravity);
        var timeElapsed = 0f;
        var velocityX = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(20f * Mathf.Deg2Rad);
        var velocityY = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(20f * Mathf.Deg2Rad);
        var projectileDuration = projectileDistance / velocityX;

        var enemyLaunchPoint = enemy.transform.localPosition;

        // play animation here

        var fleshChunkObj = Instantiate(fleshChunk, enemyLaunchPoint, enemy.transform.rotation);
        fleshChunkObj.transform.parent = enemyAbilities.transform;

        var fleshChunkValues = fleshChunkObj.GetComponent<ProjectileDamage>();
        fleshChunkValues.enemyMove = enemyMove;
        fleshChunkValues.enemyValues = enemyInfo.enemyVariable;

        while (timeElapsed < projectileDuration)
        {
            fleshChunkObj.transform.Translate(0, (velocityY - (gravity * timeElapsed)) * Time.deltaTime, velocityX * Time.deltaTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // Debug.Log("used fleshchunkthrow");
        Destroy(fleshChunkObj);
    }

    public IEnumerator kickAttack(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();
        var playerHealth = player.GetComponent<TestHealth>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;

        // play animation here

        navMeshEnemy.speed = 0f;

        yield return new WaitForSeconds(attackInterval);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;

        // Debug.Log("used kickattack");
        yield return null;
    }

    public IEnumerator lungeAttack(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;

        var playerHealth = player.GetComponent<TestHealth>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;
        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();

        // play animation here

        navMeshEnemy.speed = 0;

        yield return new WaitForSeconds(1f);

        navMeshEnemy.speed = enemyMove.c_skillSpeed; // increase speed to make it look like a lunge attack

        yield return new WaitForSeconds(1f);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = 0; // pause enemy after 'lunge attack'

        yield return new WaitForSeconds(1f);

        navMeshEnemy.speed = originalSpeed; // back to chasing player at normal speed

        // Debug.Log("used lungeattack");
        yield return null;
    }

    public IEnumerator meleeWeaponAttack(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();        
        var playerHealth = player.GetComponent<PlayerHealth>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;

        // play animation here

        navMeshEnemy.speed = 0f;

        yield return new WaitForSeconds(attackInterval);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;

        // Debug.Log("used meleeweaponattack");
        yield return null;
    }

    public IEnumerator punchAttack(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();
        var playerHealth = player.GetComponent<TestHealth>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;

        // play animation here

        navMeshEnemy.speed = 0f;

        yield return new WaitForSeconds(attackInterval);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;

        // Debug.Log("used punchattack");
        yield return null;
    }

    // BOSS SKILLS BELOW

    public IEnumerator shieldWalk(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, EnemyCommon enemyCommon)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();

        // play animation here


        enemyCommon.takeLessDamage = true;
        enemyCommon.damageReductionMultiplier = enemyMove.c_skillDamageReductionMultiplier;
        navMeshEnemy.speed = navMeshEnemy.speed - enemyMove.c_skillSpeed;
        
        yield return new WaitForSeconds(enemyMove.c_skillDuration);

        enemyCommon.takeLessDamage = false;
        navMeshEnemy.speed = originalSpeed;

        yield return null;
    }

    public IEnumerator spearPlunge(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();
        var playerHealth = player.GetComponent<TestHealth>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;

        // play animation here (spear thrust forward)

        navMeshEnemy.speed = 0f;

        yield return new WaitForSeconds(attackInterval);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;

        // Debug.Log("used spearplunge");
        yield return null;
    }

    public IEnumerator spearSweep (EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();
        var playerHealth = player.GetComponent<TestHealth>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;
        var rigidbody = enemy.GetComponent<Rigidbody>();
        var yPosition = enemy.transform.rotation.y;
        var sweepPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 2.9f, enemy.transform.position.z);
        var enemyChase = enemy.GetComponent<EnemyChase>();
        
        navMeshEnemy.speed = 0f;
        enemyChase.stopRotate = true;

        var spearSweepIndicatorObj = Instantiate(spearSweepIndicator, sweepPosition, enemy.transform.rotation);
        var spearScript = spearSweepIndicatorObj.GetComponent<SweepIndicator>();
        spearScript.enemyMove = enemyMove;

        spearSweepIndicatorObj.transform.rotation = Quaternion.Euler(90f, spearSweepIndicatorObj.transform.eulerAngles.y, yPosition);
        spearSweepIndicatorObj.transform.parent = enemyAbilities.transform;

        yield return new WaitForSeconds(enemyMove.c_skillDuration); // need to change c_skillduration to change lifespan of skill

        if (spearScript.playerTakeDamage == true && Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;
        enemyChase.stopRotate = false;

        Destroy(spearSweepIndicatorObj);

        yield return null;
    }

    public IEnumerator spinningStrike(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;

        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();
        var enemyChase = enemy.GetComponent<EnemyChase>();
        var playerHealth = player.GetComponent<TestHealth>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;
        var spinPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 2.9f, enemy.transform.position.z);
        var Scale = new Vector3(8, 0.01f, 8);

        navMeshEnemy.speed = 0f;
        enemyChase.stopRotate = true;        

        var spinIndicatorObj = Instantiate(warningObject, spinPosition, enemy.transform.rotation);

        spinIndicatorObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        spinIndicatorObj.transform.localScale = Scale;
        spinIndicatorObj.transform.parent = enemyAbilities.transform;

        var spinScript = spinIndicatorObj.AddComponent<SweepIndicator>();
        spinScript.enemyMove = enemyMove;
        spinScript.isSpinAttack = true;
        spinScript.enemy = enemy;

        yield return new WaitForSeconds(1f); // need to change c_skillduration to change lifespan of skill

        if (spinScript.playerTakeDamage == true && Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;
        enemyChase.stopRotate = false;

        // Destroy(spinIndicatorObj);

        yield return null;
    }

    public IEnumerator strikeDash(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        var playerHealth = player.GetComponent<TestHealth>();
        var enemyChase = enemy.GetComponent<EnemyChase>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;
        var LungeLocations = player.transform.Find("LungeLocations");
        Vector3 lungePosition;
        Vector3 target;

        bool hitPlayer = false;
        float elapsedTime = 0f;
        float waitTime = 0.5f; // takes 0.5s to dash to location
        
        List<float> LungeList = new List<float>();

        foreach (Transform lungeLocation in LungeLocations)
        {
            var lungeDistance = Vector3.Distance(enemy.transform.position, lungeLocation.position);
            LungeList.Add(lungeDistance);
        }

        var maxDistance = LungeList.Max();

        foreach (Transform lungeLocation in LungeLocations)
        {
            if (Vector3.Distance(enemy.transform.position, lungeLocation.position) == maxDistance)
            {
                lungePosition = new Vector3(lungeLocation.position.x, lungeLocation.position.y + 2f, lungeLocation.position.z);
                var currentPosition = enemy.transform.position;
                target = new Vector3(lungeLocation.position.x, enemy.transform.position.y, lungeLocation.position.z);

                enemy.transform.LookAt(target);
                enemyChase.stopRotate = true;

                while (elapsedTime < waitTime)
                {
                    enemy.transform.position = Vector3.Lerp(currentPosition, lungePosition, (elapsedTime / waitTime));
                    elapsedTime += Time.deltaTime;

                    if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
                    {
                        hitPlayer = true;
                    }

                    yield return null;
                }

                if (hitPlayer == true)
                {
                    playerHealth.TakeDamage(skillDamage);
                }

                enemyChase.stopRotate = false;
            }
        }

        yield return null;
    }

    public IEnumerator threeStrikeCombo(EnemyMoves enemyMove, GameObject enemy, EnemyInfo enemyInfo, SphereCollider attackRange)
    {
        enemyMove.c_skillCooldown = enemyMove.o_skillCooldown;
        var navMeshEnemy = enemy.GetComponent<NavMeshAgent>();
        var playerHealth = player.GetComponent<TestHealth>();
        var skillDamage = enemyInfo.enemyVariable.c_enemyDamage * enemyMove.c_skillDamageMultiplier;

        // play animation here (three attacks)

        navMeshEnemy.speed = 0f;

        yield return new WaitForSeconds(attackInterval);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;

        yield return new WaitForSeconds(attackInterval);

        navMeshEnemy.speed = 0f;

        yield return new WaitForSeconds(attackInterval);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;

        yield return new WaitForSeconds(attackInterval);

        navMeshEnemy.speed = 0f;

        yield return new WaitForSeconds(attackInterval);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= attackRange.radius)
        {
            playerHealth.TakeDamage(skillDamage);
        }

        navMeshEnemy.speed = originalSpeed;

        // Debug.Log("used spearplunge");
        yield return null;
        yield return null;
    }

}
