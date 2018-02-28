public class BaseCharacterClass {

    private string characterClassName;
    private string characterClassDescription;
    public bool customClass;
    public int[] stats;

    //stats
    public AttributeTypes attributeTypes;

    public string CharacterClassName{
        get {return characterClassName;}
        set {characterClassName = value;}
    }
    public string CharacterClassDescription{
        get {return characterClassDescription;}
        set {characterClassDescription = value;}
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
    /*STRENGTH, // Increases meleedamage, and increases how much weight you can have in your bags, more weight = more bag space && how heavy armor you can carry, heavier armor gives more defense
    STAMINA, //Increases healthpool
    INTELLECT, //Allmagic damage increase, Increase mana pool && give memory
    AGILITY, // +meleeattackdamage / 2 of str, + crit + movementspeed
    HASTE, // how fast you cast / can melee swing
    DEFENSE, // reduction to physical damage (dark, physical, bleed, poison)
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
    }
}﻿