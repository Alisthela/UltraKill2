using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSwordScript : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float attackCooldown = .3f;
    public bool isAttacking = false;
    public int damage;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            /*
            Enemy.EnemyHealth enemyHealth = other.gameObject.GetComponent<Enemy.EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                enemyHealth.Burning();
            }
            */
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(CanAttack)
            {
                SwordAttack();
            }
        }
    }
    
    public void SwordAttack()
    {
        isAttacking = true;
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        //anim.speed = Pause.Acceleration;
        StartCoroutine(ResetAttackCooldown());
    }
    
    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }
    
    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}