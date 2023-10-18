using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings : MonoBehaviour
{
    private string input;

    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input == "")
        {
            
        }
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }
}
