using UnityEngine;

public class BaseCharacterClass : ScriptableObject{

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


    //stats
    [HideInInspector]
    public AttributeTypes attributeTypes;

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
    HASTE, // how fast you cast / can melee swing
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

    void StatCalc()
    {
        //calc here how stats impact eachother, like stamina giving hp and such

    }
}﻿