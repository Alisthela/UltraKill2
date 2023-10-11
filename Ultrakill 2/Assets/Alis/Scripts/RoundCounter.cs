using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    public static RoundCounter instance;

    public int roundNumber;

    public bool oneTime = false;

    public bool gameOver = false;
    public bool gameEnd = false;

    public Transform Enemies;

    public Transform enemyAbilities;

    public float timePlayed;

    private void Start()
    {
        instance = this;
        oneTime = true;
        GameStart();

        Save.instance.LoadFile(); // load the previous data
        Save.instance.SaveFile(); // save new data
        Save.instance.LoadFile(); // make sure it has been saved
    }


    private void Update()
    {
        if (EnemySpawn.instance.Enemies.Count == 0 && oneTime == false && gameOver == false)
        {
            StartCoroutine("RoundChange");
            oneTime = true;
        }

        if (gameOver == false)
        {
            timePlayed += Time.deltaTime;
        }

        if (gameOver == true && gameEnd == false)
        {
            gameEnd = true;
            Save.instance.SaveFile();
            Save.instance.LoadFile();
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
        NextRound();

        if (enemyAbilities.childCount > 0)
        {
            for (int i = 0; i < enemyAbilities.childCount; i++)
            {
                Destroy(enemyAbilities.GetChild(i).gameObject);
            }
        }
    }
}
