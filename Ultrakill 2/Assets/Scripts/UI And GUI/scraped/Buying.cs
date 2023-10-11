/*

using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buying : MonoBehaviour
{
    public GameManager gameManager;

    public float S_Cost;

    public float S_Amount;

    public bool Itembought = false;

    [SerializeField] GameObject ItemBeingPurchased;

    public enum BuyType
    {
        Pistol
    };

    public BuyType S_BuyType;

    public BuyType State { get { return S_BuyType; } }

    // Start is called before the first frame update
    void Start()
    {
        ItemBeingPurchased.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (S_BuyType)
        {
            case BuyType.Pistol:
                if (Itembought == true)
                {
                    ItemBeingPurchased.SetActive(true);
                    gameManager.AmmoAmount += 50;
                    Itembought = false;
                }

                break;
        }
    }

    public void BuyItem()
    {
        if (gameManager.MoneyAmount >= S_Cost)
        {
            Itembought = true;
            gameManager.MoneyAmount -= S_Cost;
        }
    }
}
*/