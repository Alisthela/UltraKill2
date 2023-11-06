using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;
using static UpgradeCard;

public class UpgradeCard : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameManager gameManager;

    public UpgradeCard upgradeCard1;
    public UpgradeCard upgradeCard2;
    public UpgradeCard upgradeCard3;

    public bool iscardselected = false;

    public bool iscardsative = false;

    public bool cardchoosen = false;

    public bool CardConforimed = false;

    public GunData PistolData;

    public GunData ShotGunData;

    public GunData RifleGunData;

    public GameObject cardbackground;

    public TMP_Text cardheader;
    public TMP_Text cardDescription;

    public float randomnumber;
    public bool baseUpgradecardselected = false;
    public bool isplayer = false;
    public bool isgun = false;
    public bool isenemy = false;
    public bool ispistol = false;
    public bool isshotgun = false;
    public bool isrifle = false;

    public enum whitchupgradecard
    {
        card1,
        card2,
        card3
    }
    public enum UpgradeCardVarient
    {
        Player,
        Gun,
        Enemy
    };

    public enum PlayerCardVarient
    {
        Health,
        Walk_speed,
        Slide_speed,
        Wallrun_speed,
        Jump_force,
        Air_Jump,
    };

    public enum GunType
    {
        Pistol,
        Shotgun,
        Assault_rifle
    };

    public enum pistolCardVarient
    {
        Damage,
        Mag_size,
        Reload_time,
        Special
    };

    public enum shotgunCardVarient
    {
        Damage,
        Mag_size,
        Reload_time,
        Special
    };

    public enum rifleCardVarient
    {
        Damage,
        Mag_size,
        Reload_time,
        Special
    };

    public enum EnemeyCardVarient
    {

    };

    public UpgradeCardVarient m_CardVarient;
    public PlayerCardVarient M_playercardvarient;
    public GunType m_guntype;
    public pistolCardVarient M_pistolCardVarient;
    public shotgunCardVarient M_shotgunCardVarient;
    public rifleCardVarient M_rifleCardVarient;
    public EnemeyCardVarient M_EnemeyCardVarient;
    public whitchupgradecard M_whitchcard;

    // Start is called before the first frame update
    void Start()
    {
        cardbackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_CardVarient)
        {
            case UpgradeCardVarient.Player:
                switch (M_playercardvarient)
                {
                    case PlayerCardVarient.Health:
                        if (cardchoosen == true && CardConforimed == true)
                        {
                            gameManager.cardeffectdone = true;
                        }
                        cardheader.text = "Health";
                        cardDescription.text = "Increase health by 10 (not in game)";
                        iscardselected = true;
                        break;
                    case PlayerCardVarient.Walk_speed:
                        if (cardchoosen == true && CardConforimed == true)
                        {
                            playerMovement.walkSpeed += 1;
                            gameManager.cardeffectdone = true;
                         }
                        cardheader.text = "Walk Speed";
                        cardDescription.text = "Increase Walk speed by 1";
                        iscardselected = true;
                        break;
                    case PlayerCardVarient.Wallrun_speed:
                        if (cardchoosen == true && CardConforimed == true)
                        {
                            playerMovement.wallrunSpeed += 1;
                            gameManager.cardeffectdone = true;
                        }
                cardheader.text = "Wallrun Speed";
                        cardDescription.text = "Increase Wallrun speed by 1";
                        iscardselected = true;
                        break;
                    case PlayerCardVarient.Jump_force:
                        if (cardchoosen == true && CardConforimed == true)
                        {
                            playerMovement.jumpForce += 2; 
                            gameManager.cardeffectdone = true;
                        }
                        cardheader.text = "Jump Force";
                        cardDescription.text = "Increase Jump Height by 2";
                        iscardselected = true;
                        break;
                    case PlayerCardVarient.Air_Jump:
                        if (cardchoosen == true && CardConforimed == true)
                        {
                            playerMovement.airJumps += 1;
                            gameManager.cardeffectdone = true;
                        }
                        cardheader.text = "Air Jump";
                        cardDescription.text = "Increase how many times you can jump mid air";
                        iscardselected = true;
                        break;
                }
                break;
            case UpgradeCardVarient.Gun:
                switch (m_guntype)
                {
                    case GunType.Pistol:
                        switch (M_pistolCardVarient)
                        {
                            case pistolCardVarient.Damage:
                                if (cardchoosen == true && CardConforimed == true)
                                {
                                    PistolData.damage += 3;
                                    gameManager.cardeffectdone = true;
                                }
                                cardheader.text = "Pistol Damage";
                                cardDescription.text = "Increase pistol damage";
                                iscardselected = true;
                                break;
                            case pistolCardVarient.Mag_size:
                                if (cardchoosen == true && CardConforimed == true)
                                {
                                    PistolData.magSize += 4;
                                    gameManager.cardeffectdone = true;
                                }
                                cardheader.text = "Pistol Mag Size";
                                cardDescription.text = "Increase pistol mag size";
                                iscardselected = true;
                                break;
                            //case pistolCardVarient.Reload_time:
                            //    PistolData.reloadTime -= 1;
                            //    cardheader.text = "Pistol Reload Time";
                            //    cardDescription.text = "Decrease pistol Reload time";
                            //    iscardselected = true;
                            //    break;
                           // case GunCardVarient.Special:
                                
                            //    break;
                        }
                        break;
                    case GunType.Shotgun:
                        switch (M_shotgunCardVarient)
                        {
                            case shotgunCardVarient.Damage:
                                if (cardchoosen == true && CardConforimed == true)
                                {
                                    ShotGunData.damage += 3;
                                    gameManager.cardeffectdone = true;
                                }
                                cardheader.text = "Shotgun Damage";
                                cardDescription.text = "Increase Shotgun damage";
                                iscardselected = true;
                                break;
                            case shotgunCardVarient.Mag_size:
                                if (cardchoosen == true && CardConforimed == true)
                                {
                                    ShotGunData.magSize += 1;
                                    gameManager.cardeffectdone = true;
                                }
                                cardheader.text = "ShotGun Mag Size";
                                cardDescription.text = "Increase Shotgun mag size";
                                iscardselected = true;
                                break;
                            case shotgunCardVarient.Reload_time:
                                if (cardchoosen == true && CardConforimed == true)
                                {
                                    ShotGunData.reloadTime -= 1;
                                    gameManager.cardeffectdone = true;
                                }
                                cardheader.text = "Shotgun Reload Time";
                                cardDescription.text = "Decrease Shotgun Reload time";
                                iscardselected = true;
                                break;
                            //case GunCardVarient.Special:

                               // break;
                        }
                        break;
                    case GunType.Assault_rifle:
                        switch (M_rifleCardVarient)
                        {
                            case rifleCardVarient.Damage:
                                if (cardchoosen == true && CardConforimed == true)
                                {
                                    RifleGunData.damage += 3;
                                    gameManager.cardeffectdone = true;
                                }
                                cardheader.text = "Rifle Damage";
                                cardDescription.text = "Increase Rifle damage";
                                iscardselected = true;
                                break;
                            case rifleCardVarient.Mag_size:
                                if (cardchoosen == true && CardConforimed == true)
                                {
                                    RifleGunData.magSize += 5;
                                    gameManager.cardeffectdone = true;
                                }
                                cardheader.text = "Rifle Mag Size";
                                cardDescription.text = "Increase Rifle mag size";
                                iscardselected = true;
                                break;
                            case rifleCardVarient.Reload_time:
                                if (cardchoosen == true && CardConforimed == true)
                                {
                                    RifleGunData.reloadTime -= 1;
                                    gameManager.cardeffectdone = true;
                                }
                                cardheader.text = "Rifle Reload Time";
                                cardDescription.text = "Decrease Rifle Reload time";
                                iscardselected = true;
                                break;
                            //case GunCardVarient.Special:
                                
                             //   break;
                        }
                        break;
                }
                break;
            case UpgradeCardVarient.Enemy:
                switch (M_EnemeyCardVarient)
                {

                }
                break;
        }

        switch (M_whitchcard)
        {
            case whitchupgradecard.card1:
                if (cardchoosen == true)
                {
                    upgradeCard2.cardchoosen = false;
                    upgradeCard3.cardchoosen = false;
                }
                break;
            case whitchupgradecard.card2:
                if (cardchoosen == true)
                {
                    upgradeCard1.cardchoosen = false;
                    upgradeCard3.cardchoosen = false;
                }
                break;
            case whitchupgradecard.card3:
                if (cardchoosen == true)
                {
                    upgradeCard2.cardchoosen = false;
                    upgradeCard1.cardchoosen = false;;
                }
                break;
        }
        if (cardchoosen == false)
        {
            cardbackground.SetActive(false);
            CardConforimed = false;
        }
        else if (cardchoosen == true)
        {
            cardbackground.SetActive(true);
        }

        if (iscardsative == true)
        {
            randomnumber = Random.Range(1, 4);
            if (randomnumber == 1)
            {
                m_CardVarient = UpgradeCardVarient.Player;
                isplayer = true;
                baseUpgradecardselected = true;
            }
            else if (randomnumber == 2)
            {
                m_CardVarient = UpgradeCardVarient.Gun;
                isgun = true;
                baseUpgradecardselected = true;
            }
            else if (randomnumber == 3)
            {
                m_CardVarient = UpgradeCardVarient.Enemy;
                isenemy = true;
                baseUpgradecardselected = true;
            }

            if (baseUpgradecardselected == true)
            {
                if (isplayer == true)
                {
                    randomnumber = Random.Range(1, 7);
                    if (randomnumber == 1)
                    {
                        M_playercardvarient = PlayerCardVarient.Health;
                        iscardsative = false;
                    }
                    else if (randomnumber == 2)
                    {
                        M_playercardvarient = PlayerCardVarient.Air_Jump;
                        iscardsative = false;
                    }
                    else if (randomnumber == 3)
                    {
                        M_playercardvarient = PlayerCardVarient.Jump_force;
                        iscardsative = false;
                    }
                    else if (randomnumber == 4)
                    {
                        M_playercardvarient = PlayerCardVarient.Walk_speed;
                        iscardsative = false;
                    }
                    else if (randomnumber == 5)
                    {
                        M_playercardvarient = PlayerCardVarient.Slide_speed;
                        iscardsative = false;
                    }
                    else if (randomnumber == 6)
                    {
                        M_playercardvarient = PlayerCardVarient.Wallrun_speed;
                        iscardsative = false;
                    }

                }
                else if (isgun == true)
                {
                    randomnumber = Random.Range(1, 4);
                    if (randomnumber == 1)
                    {
                        m_guntype = GunType.Pistol;
                        ispistol = true;
                    }
                    else if (randomnumber == 2)
                    {
                        m_guntype = GunType.Shotgun;
                        isshotgun = true;
                    }
                    else if (randomnumber == 3)
                    {
                        m_guntype = GunType.Assault_rifle;
                        isrifle = true;
                    }
                }
                /*
                else if (isenemy == true)
                {
                    randomnumber = Random.RandomRange(1, 2);
                    if (randomnumber == 1)
                    {
                        M_EnemeyCardVarient = EnemeyCardVarient
                    }

                }
                */

                if (ispistol == true)
                {
                    randomnumber = Random.Range(1, 4);
                    if (randomnumber == 1)
                    {
                        M_pistolCardVarient = pistolCardVarient.Damage;
                        iscardsative = false;
                    }
                    else if (randomnumber == 2)
                    {
                        M_pistolCardVarient = pistolCardVarient.Reload_time;
                        iscardsative = false;
                    }   
                    else if (randomnumber == 3)
                    {
                        M_pistolCardVarient = pistolCardVarient.Mag_size;
                        iscardsative = false;
                    }
                }
                else if (isshotgun == true)
                {
                    randomnumber = Random.Range(1, 4);
                    if (randomnumber == 1)
                    {
                        M_shotgunCardVarient = shotgunCardVarient.Damage;
                        iscardsative = false;
                    }
                    else if (randomnumber == 2)
                    {
                        M_shotgunCardVarient = shotgunCardVarient.Reload_time;
                        iscardsative = false;
                    }
                    else if (randomnumber == 3)
                    {
                        M_shotgunCardVarient = shotgunCardVarient.Mag_size;
                        iscardsative = false;
                    }
                }
                else if (isrifle == true)
                {
                    randomnumber = Random.Range(1, 4);
                    if (randomnumber == 1)
                    {
                        M_rifleCardVarient = rifleCardVarient.Damage;
                        iscardsative = false;
                    }
                    else if (randomnumber == 2)
                    {
                        M_rifleCardVarient = rifleCardVarient.Reload_time;
                        iscardsative = false;
                    }
                    else if (randomnumber == 3)
                    {
                        M_rifleCardVarient= rifleCardVarient.Mag_size;
                        iscardsative = false;
                    }
                }
            }
        }
    }

    public void CardSelected()
    {
        if (cardchoosen == false)
        {
            cardchoosen = true;
        }
        else if (cardchoosen == true)
        {
            cardchoosen = false;
        }
    }
}
