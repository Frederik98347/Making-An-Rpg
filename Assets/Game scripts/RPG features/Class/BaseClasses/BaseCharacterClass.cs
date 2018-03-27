using UnityEngine;

public class BaseCharacterClass : BasePlayer{

    public GameObject charModel;
    private string characterClassName;
    private string characterClassDescription;
    public bool customClass;
    [HideInInspector]
    public int[] stats;

    int maxhealth;
    int curHealth;
    float runningSpeed;
    float walkingSpeed;
    float attackspeed;
    float haste;

    float globalCoolDown = 1.5f;

    int staminaFormula;

    //stats
    [HideInInspector]
    public AttributeTypes attributeTypes;
    private int baseHealth = 5;
    private float baseAttackSpeed = 2f;
    private float attackSpeedFormula;
    int level = 1;
    private float defenseFormula;
    private float defense;
    private float castingSpeedFormula;
    private float baseCastingSpeed = 2f;
    private float resistanceFormula;
    private int _pointsIntoStam = 1;
    private float _staminaFormula;

    public string CharacterClassName{
        get {return characterClassName;}
        set {characterClassName = value;}
    }
    public string CharacterClassDescription{
        get {return characterClassDescription;}
        set {characterClassDescription = value;}
    }

    public int Maxhealth
    {
        get
        {
            return maxhealth;
        }

        set
        {
            maxhealth = value;
        }
    }

    public int CurHealth
    {
        get
        {
            return curHealth;
        }

        set
        {
            curHealth = value;
        }
    }

    public float Attackspeed
    {
        get
        {
            return attackspeed;
        }

        set
        {
            attackspeed = value;
        }
    }

    public float RunningSpeed
    {
        get
        {
            return runningSpeed;
        }

        set
        {
            runningSpeed = value;
        }
    }

    public float WalkingSpeed
    {
        get
        {
            return walkingSpeed;
        }

        set
        {
            walkingSpeed = value;
        }
    }

    public int BaseHealth
    {
        get
        {
            return baseHealth;
        }

        set
        {
            baseHealth = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public float GlobalCoolDown
    {
        get
        {
            return globalCoolDown;
        }

        set
        {
            globalCoolDown = value;
        }
    }

    public float Defense
    {
        get
        {
            return defense;
        }

        set
        {
            defense = value;
        }
    }

    public float Haste
    {
        get
        {
            return haste;
        }

        set
        {
            haste = value;
        }
    }

    public float CastingSpeed
    {
        get
        {
            return baseCastingSpeed;
        }

        set
        {
            baseCastingSpeed = value;
        }
    }

    public float Resistance
    {
        get
        {
            return resistanceFormula;
        }

        set
        {
            resistanceFormula = value;
        }
    }

    void CustomClass()
    {
        if (customClass)
        {
            CharClassGen();
        }
    }

    void CharClassGen()
    {
        //Generate name for costum made class & stats / bonuses for it
    }

    public void Stats()
    {
    /*STRENGTH, // Increases meleedamage, increase defence aswell.
    STAMINA, //Increases healthpool
    INTELLECT, //Allmagic damage increase, Increase energy pool & energy regen
    AGILITY, // +meleeattackdamage / 2 of str + movementspeed
    HASTE, // how fast you cast / can melee swing, reduce gcd
    DEFENSE, // reduction to physical damage
    CRITCHANCE, //how often you critical strike
    ENDURANCE, // how long time you can sprint
    RESISTANCE, // reduction to all magic damage by x%
    MEMORY // How many skills have active at the same time, meaning not restricting how many you can learn, but how many you can use.
    */
        stats = new int[10];
        stats[0] = (int)AttributeTypes.STRENGTH;
        stats[1] = (int)AttributeTypes.STAMINA;
        stats[2] = (int)AttributeTypes.INTELLECT;
        stats[3] = (int)AttributeTypes.AGILITY;
        stats[4] = (int)AttributeTypes.HASTE;
        stats[5] = (int)AttributeTypes.DEFENSE;
        stats[6] = (int)AttributeTypes.CRITCHANCE;
        stats[7] = (int)AttributeTypes.ENDURANCE;
        stats[8] = (int)AttributeTypes.RESISTANCE;
        stats[9] = (int)AttributeTypes.MEMORY;

        //make sure all stats = 0 on init
        InitStats();

        StatCalc();
    }

    private void InitStats()
    {
        //basic stat template for all classes
        stats[0] = 6;
        stats[1] = 8;
        stats[2] = 6;
        stats[3] = 6;
        stats[4] = 4;
        stats[5] = 4;
        stats[6] = 3;
        stats[7] = 3;
        stats[8] = 4;
        stats[9] = 5;
    }

    public void StatCalc()
    {
        Level = PlayerLevel;

        //calc here how stats impact eachother, like stamina giving hp and such
        //formulas for calculation
        staminaFormula = stats[1];
        _staminaFormula = stats[1] * 1.25f;
        attackSpeedFormula = baseAttackSpeed / (1 + (stats[4] / 100));
        castingSpeedFormula = baseCastingSpeed / (1 + (stats[4] / 100));
        defenseFormula = (0.01050120510299f * stats[5] + 0.003205956904f);
        resistanceFormula = (0.01050120510299f * stats[8] + 0.003105956904f);

        //Increases healthpool
        if (stats[1] > 49)
        {
            staminaFormula += (int)(_staminaFormula / 2f);
            Maxhealth = BaseHealth + (staminaFormula);
        } else
        {
            staminaFormula = stats[1];
            Maxhealth = BaseHealth + (staminaFormula);
        }

        //Haste
        Haste = stats[4] / (1 + (stats[4] / 100));
        // how fast you cast / can melee swing
        Attackspeed = (attackSpeedFormula + 1/5*Haste) / Level;
        CastingSpeed = (castingSpeedFormula + 1 / 5 * Haste) / Level;

        //defense stats

        // reduction to physical damage
        Defense = defenseFormula / Level;

        //reduction to all magic damage
        Resistance = resistanceFormula / Level;

    }
}﻿