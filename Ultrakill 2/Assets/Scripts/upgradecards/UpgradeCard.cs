using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    public bool iscardsative = false;
    public enum UpgradeCardVarient
    {
        health,
        damage,
        speed,
        test,
    };

    public UpgradeCardVarient m_CardVarient;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_CardVarient)
        {
            case UpgradeCardVarient.health:

                break;
        }
    }
}
