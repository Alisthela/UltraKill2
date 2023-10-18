using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepIndicator : MonoBehaviour
{
    public bool playerTakeDamage;
    public bool isSpinAttack;
    public GameObject enemy;
    public EnemyMoves enemyMove;

    private void Start()
    {
        Destroy(this.gameObject, enemyMove.c_skillDuration);
    }

    private void Update()
    {
        if (isSpinAttack == true)
        {
            var spinPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 2.9f, enemy.transform.position.z);
            this.transform.position = spinPosition;
        }
    }
    private void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.transform.tag == "Player")
        {
            playerTakeDamage = true;
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.transform.tag == "Player")
        {
            playerTakeDamage = false;
        }
    }
}
