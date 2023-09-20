using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Enemy/Create New Skill")]
public class EnemyMoves : ScriptableObject
{
    public string skillID;
    public string skillName;

    [Space]

    public float c_skillCooldown; // c = current, o = original (non changing)
    public float c_skillDamageMultiplier; // skill damage is based off of enemy damage so it is instead a multiplier
    public float c_skillDamageReductionMultiplier; // when skill has damage reduction effects
    public float c_skillSpeed; // for movement skills, also projectile skill speed
    public float c_skillAOE; // for skills that affect a wide area (especially explosion), used like the scale tool in unity editor e.g scale of 5
    public float c_skillDuration; // how long skills will last

    // leave at 0 if not applicable to skill
    [Space]

    public float o_skillCooldown;
    public float o_skillDamageMultiplier;
    public float o_skillDamageReductionMultiplier;
    public float o_skillSpeed;
    public float o_skillAOE;
    public float o_skillDuration;

    [Space]

    public SkillType skillType;

    [Space]

    public bool startCooldown = false;

    public enum SkillType
    {
        // put skill types here

        movementType,
        meleeType,
        projectileType,
        chargeType,
        explosionType,
        homingProjectileType,
        statusType

    }


}
