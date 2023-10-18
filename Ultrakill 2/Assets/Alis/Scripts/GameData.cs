using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerScore;
    public string playerName;
    public float timePlayed;

    public GameData(int playerScoreInt, string playerNameString, float timePlayedFloat)
    {
        playerScore = playerScoreInt;
        playerName = playerNameString;
        timePlayed = timePlayedFloat;
    }
}
