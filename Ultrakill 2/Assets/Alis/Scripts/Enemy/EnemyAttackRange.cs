using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    private List<EnemyMoves> objEnemyMoves;
    private List<EnemyMoves> availableMoves = new List<EnemyMoves>();
    public List<EnemyMoves> enemyMoves;
    public EnemyMoves enemyMove;
    public EnemyInfo enemyInfo;
    public EnemyCommon enemyCommon;
    private string skillID;
    private GameObject enemy;
    private int skillNumber;
    private bool hasRun;
    private SphereCollider attackRange;

    private void Start()
    {
        enemyInfo = this.GetComponentInParent<EnemyInfo>();
        enemyCommon = this.GetComponentInParent<EnemyCommon>();
        objEnemyMoves = enemyInfo.enemyVariable.enemyMoves;
        enemy = this.transform.parent.gameObject;
        attackRange = this.GetComponent<SphereCollider>();

        TransferSkillData();
    }

    private void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.transform.tag == "Player")
        {
            if (hasRun == false)
            {
                hasRun = true;

                CheckCooldowns();

                if (availableMoves.Count > 0)
                {
                    ChooseSkill();

                    switch (skillID)
                    {
                        case "1":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.dashTowardsPlayer(enemyMove, enemy));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "2":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.lungeAttack(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "3":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.kickAttack(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "4":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.punchAttack(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "5":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.meleeWeaponAttack(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "6":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.fleshChunkThrow(enemyMove, enemy, enemyInfo));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "7":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.bloodPool(enemyMove, enemyInfo));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "8":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.bloodExplosion(enemyMove, enemyInfo));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "9":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.shieldWalk(enemyMove, enemy, enemyInfo, enemyCommon));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "10":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.spinningStrike(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "11":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.strikeDash(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "12":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.threeStrikeCombo(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "13":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.spearPlunge(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }

                        case "14":
                            {
                                if (enemyMove.c_skillCooldown == 0)
                                {
                                    StartCoroutine(EnemySkills.instance.spearSweep(enemyMove, enemy, enemyInfo, attackRange));
                                    availableMoves.Clear();
                                }
                                break;
                            }
                        default:
                            {
                                Debug.Log("Abilities are on cooldown.");
                                break;
                            }
                    }
                }           

                StartCoroutine("UpdateBool");
            }
            
        }
    }


    private void ChooseSkill()
    {
        skillNumber = UnityEngine.Random.Range(0, availableMoves.Count);
        skillID = availableMoves[skillNumber].skillID;
        enemyMove = availableMoves[skillNumber];
    }

    private void CheckCooldowns()
    {
        foreach (EnemyMoves enemyMove in enemyMoves)
        {
            if (enemyMove.c_skillCooldown == 0)
            {
                availableMoves.Add(enemyMove);
            }
        }
    }
    
    private void TransferSkillData()
    {
        foreach (EnemyMoves enemyMove in objEnemyMoves)
        {
            enemyMoves.Add(Instantiate(enemyMove));
        }
    }

    private IEnumerator UpdateBool()
    {
        yield return new WaitForSeconds(1f);
        hasRun = false;
    }
}
