using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyChase : MonoBehaviour
{

    public Vector3 playerPosition;
    public NavMeshAgent enemy;
    public Collider enemyCollider;
    public SphereCollider enemyAttackRange;
    public GameObject enemyAttackRangeObj;
    public float stoppingDistance;
    public bool stopRotate;

    private Quaternion lookRotation;
    private float rotateSpeed = 0.07f;

    private void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        enemyAttackRangeObj = FindAttackRangeObj();
        enemyAttackRange = enemyAttackRangeObj.GetComponent<SphereCollider>();
        stoppingDistance = enemyAttackRange.radius - 0.1f;
        enemy.stoppingDistance = stoppingDistance;

        enemy.updateRotation = false;
    }
    private void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Player")
        {
            playerPosition = collisionInfo.transform.position;
            enemy.destination = playerPosition;

            if (stopRotate == false)
            {
                StartCoroutine(Rotate());
            }
        }
    }

    private GameObject FindAttackRangeObj()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).tag == "AttackRange")
            {
                return this.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    private IEnumerator Rotate()
    {
        lookRotation = Quaternion.LookRotation(playerPosition - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed);

        yield return null;
    }
}
