using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerName : MonoBehaviour
{
    public TMP_InputField playerInput;
    public static UIPlayerName instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (playerInput.text.Length > 0)
        {
            Save.instance.playerName = playerInput.text;
            Debug.Log("Player name is: " + playerInput.text);
        }
    }
}
