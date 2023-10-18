using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UpgradeCard;

public class UpgradeCard : MonoBehaviour
{
    PlayerMovement playerMovement;

    UpgradeCard upgradeCard1;
    UpgradeCard upgradeCard2;
    UpgradeCard upgradeCard3;

    public bool iscardselected = false;

    public bool iscardsative = false;

    public GunData PistolData;

    public GunData ShotGunData;

    public GunData RifleGunData;

    public GameObject cardbackground;

    public TMP_Text cardheader;
    public TMP_Text cardDescription;

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
        Wall_run_drag
    };

    public enum GunType
    {
        Pistol,
        Shotgun,
        Assault_rifle
    };

    public enum GunCardVarient
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
    public GunCardVarient M_GunCardVarient;
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

                        cardheader.text = "Health";
                        cardDescription.text = "Increase health by 10 (not in game)";
                        iscardsative = true;
                        break;
                    case PlayerCardVarient.Walk_speed:
                        playerMovement.walkSpeed += 10;
                        cardheader.text = "Walk Speed";
                        cardDescription.text = "Increase Walk speed by 10";
                        iscardselected = true;
                        break;
                    case PlayerCardVarient.Slide_speed:
                        playerMovement.slideSpeed += 10;
                        cardheader.text = "Slide Speed";
                        cardDescription.text = "Increase Slide Speed by 10";
                        iscardselected = true;
                        break;
                    case PlayerCardVarient.Wallrun_speed:
                        playerMovement.wallrunSpeed += 10;
                        cardheader.text = "Wallrun Speed";
                        cardDescription.text = "Increase Wallrun speed by 10";
                        iscardselected = true;
                        break;
                    case PlayerCardVarient.Jump_force:
                        playerMovement.jumpForce += 10;
                        cardheader.text = "Jump Force";
                        cardDescription.text = "Increase Jump Height by 10";
                        iscardselected = true;
                        break;
                    case PlayerCardVarient.Air_Jump:
                        playerMovement.airJumps += 1;
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
                        switch (M_GunCardVarient)
                        {
                            case GunCardVarient.Damage:
                                PistolData.damage += 10;
                                cardheader.text = "Pistol Damage";
                                cardDescription.text = "Increase pistol damage";
                                iscardselected = true;
                                break;
                            case GunCardVarient.Mag_size:
                                PistolData.magSize += 10;
                                cardheader.text = "Pistol Mag Size";
                                cardDescription.text = "Increase pistol mag size";
                                iscardselected = true;
                                break;
                            case GunCardVarient.Reload_time:
                                PistolData.reloadTime -= 1;
                                cardheader.text = "Pistol Reload Time";
                                cardDescription.text = "Decrease pistol Reload time";
                                iscardselected = true;
                                break;
                           // case GunCardVarient.Special:
                                
                            //    break;
                        }
                        break;
                    case GunType.Shotgun:
                        switch (M_GunCardVarient)
                        {
                            case GunCardVarient.Damage:
                                ShotGunData.damage += 10;
                                cardheader.text = "Shotgun Damage";
                                cardDescription.text = "Increase Shotgun damage";
                                iscardselected = true;
                                break;
                            case GunCardVarient.Mag_size:
                                ShotGunData.magSize += 10;
                                cardheader.text = "ShotGun Mag Size";
                                cardDescription.text = "Increase Shotgun mag size";
                                iscardselected = true;
                                break;
                            case GunCardVarient.Reload_time:
                                ShotGunData.reloadTime += 10;
                                cardheader.text = "Shotgun Reload Time";
                                cardDescription.text = "Decrease Shotgun Reload time";
                                iscardselected = true;
                                break;
                            //case GunCardVarient.Special:

                               // break;
                        }
                        break;
                    case GunType.Assault_rifle:
                        switch (M_GunCardVarient)
                        {
                            case GunCardVarient.Damage:
                                RifleGunData.damage += 10;
                                cardheader.text = "Rifle Damage";
                                cardDescription.text = "Increase Rifle damage";
                                iscardselected = true;
                                break;
                            case GunCardVarient.Mag_size:
                                RifleGunData.magSize += 10;
                                cardheader.text = "Rifle Mag Size";
                                cardDescription.text = "Increase Rifle mag size";
                                iscardselected = true;
                                break;
                            case GunCardVarient.Reload_time:
                                RifleGunData.reloadTime += 10;
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

                break;
            case whitchupgradecard.card2:

                break;
            case whitchupgradecard.card3:

                break;
        }
    }

    public void CardSelected()
    {
        cardbackground.SetActive(true);
    }

}
