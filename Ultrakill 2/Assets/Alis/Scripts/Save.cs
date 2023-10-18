using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save instance;
    public int playerScore;
    public string playerName;
    public float timePlayed;
    public ScoreTracker scoreTracker;
    public string saveDestination;

    private void Start()
    {
        instance = this;

        scoreTracker = (GameObject.Find("EnemyManager")).GetComponent<ScoreTracker>();
        saveDestination = Application.persistentDataPath + "/save.dat";
        DontDestroyOnLoad(this);
    }

    public void SaveFile()
    {
        FileStream saveFile;

        if (File.Exists(saveDestination) == false)
        {
            saveFile = File.Create(saveDestination);
        }

        if (playerScore < scoreTracker.playerScore)
        {
            if (timePlayed <= RoundCounter.instance.timePlayed)
            {
                playerScore = scoreTracker.playerScore;
                playerName = "Bob"; //placerholder name, currently no way to set player name
                timePlayed = RoundCounter.instance.timePlayed;

                if (File.Exists(saveDestination))
                {
                    saveFile = File.OpenWrite(saveDestination);
                }
                else
                {
                    saveFile = File.Create(saveDestination);
                }

                GameData data = new GameData(playerScore, playerName, timePlayed);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(saveFile, data);
                saveFile.Close();

                Debug.Log("Data saved.");
            }          
        }
        else
        {
            Debug.Log("New score was worse, not saving.");
        }
    }

    public void LoadFile()
    {
        string saveDestination = Application.persistentDataPath + "/save.dat";
        FileStream saveFile;

        if (File.Exists(saveDestination))
        {
            saveFile = File.OpenRead(saveDestination);
        }
        else
        {
            Debug.Log("Error: Save file not found.");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        GameData data = (GameData)formatter.Deserialize(saveFile);
        saveFile.Close();

        playerScore = data.playerScore;
        playerName = data.playerName;
        timePlayed = data.timePlayed;

        Debug.Log("Top score is: " + playerScore);
        Debug.Log("Achieved by: " + playerName);
        Debug.Log("Within " + timePlayed + " seconds.");

        Debug.Log("Data loaded.");
    }
}
