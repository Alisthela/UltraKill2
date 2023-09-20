using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    public static RoundCounter instance;

    public int roundNumber;

    public bool oneTime = false;

    public Transform Enemies;

    public Transform enemyAbilities;

    private void Start()
    {
        instance = this;
        oneTime = true;
        GameStart();
    }


    private void Update()
    {
        if (EnemySpawn.instance.Enemies.Count == 0 && oneTime == false)
        {
            StartCoroutine("RoundChange");
            oneTime = true;
        }
    }


    public void GameStart()
    {
        roundNumber = 1;
        StartCoroutine(EnemySpawn.instance.SpawnEnemies(roundNumber));
    }


    public void NextRound()
    {
        Debug.Log("It is now Round: " + roundNumber);
        roundNumber++;
        EnemySpawn.instance.UpdateDifficultyValues(roundNumber);
        StartCoroutine(EnemySpawn.instance.SpawnEnemies(roundNumber));
    }
    // called when all enemies have been killed

    public IEnumerator RoundChange()
    {
        yield return new WaitForSeconds(0.8f);
        foreach(GameObject ability in enemyAbilities)
        {
            Destroy(ability);
        }

        NextRound();
    }

}
