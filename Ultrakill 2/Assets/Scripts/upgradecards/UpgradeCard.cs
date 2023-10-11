using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UpgradeCard;

public class UpgradeCard : MonoBehaviour
{
    public bool iscardsative = false;
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

    // Start is called before the first frame update
    void Start()
    {
        
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

                        break;
                    case PlayerCardVarient.Walk_speed:

                        break;
                    case PlayerCardVarient.Slide_speed:

                        break;
                    case PlayerCardVarient.Wallrun_speed:

                        break;
                    case PlayerCardVarient.Jump_force:

                        break;
                    case PlayerCardVarient.Air_Jump:

                        break;
                    case PlayerCardVarient.Wall_run_drag:

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

                                break;
                            case GunCardVarient.Mag_size:

                                break;
                            case GunCardVarient.Reload_time:

                                break;
                            case GunCardVarient.Special:

                                break;
                        }
                        break;
                        case GunType.Shotgun:
                        switch (M_GunCardVarient)
                        {
                            case GunCardVarient.Damage:

                                break;
                            case GunCardVarient.Mag_size:

                                break;
                            case GunCardVarient.Reload_time:

                                break;
                            case GunCardVarient.Special:

                                break;
                        }
                        break;
                    case GunType.Assault_rifle:
                        switch (M_GunCardVarient)
                        {
                            case GunCardVarient.Damage:

                                break;
                            case GunCardVarient.Mag_size:

                                break;
                            case GunCardVarient.Reload_time:

                                break;
                            case GunCardVarient.Special:

                                break;
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
    }
}
