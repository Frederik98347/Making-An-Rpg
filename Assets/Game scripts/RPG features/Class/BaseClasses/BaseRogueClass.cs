using RpgTools.PlayerClass;

public class BaseRogueClass : BaseCharacterClass
{
    // all generic enemy data for War class
    public PlayerDmgTypes dmgTypes;

    //warrior generic abilities
    public Spell[] RogueAbilities;

    //stat bonuses
    int StaminaBonus = -3;
    int StrengthBonus = -1;
    int IntellectBonus = -2;
    int AgilityBonus = 3;
    int CritchanceBonus = 3;
    int DefenceBonus = -2;
    int hasteBonus = 0;
    int ResistanceBonus = 0;
    int EnduranceBonus = 1;
    int MemoryBonus = 0;

    //generic base stats
    public int BaseStamina;
    public int BaseStrength;
    public int BaseIntellect;
    public int BaseAgility;
    public int BaseCritchance;
    public int BaseDefence;
    public int BaseHaste;
    public int BaseResistance;
    public int BaseEndurance;
    public int BaseMemory;

    public string ClassBonus;

    public void RogueClass()
    {
        // can take more damage, casts spells and able to avoid some damage.
        CharacterClassName = "Rogue";
        CharacterClassDescription = "A fast and agile assasasin, who deals a lot of damage in short periods of time, but in return takes massiv amounts of damage.";
        ClassBonus = "Bonuses: +15% to speed (movement & attackspeed). 10% to energy regen," +
    " Drawbacks: +5 % to physical, dark and poison damage taken.";
        StatsAfterBonus();
    }

    void StatsAfterBonus()
    {

        //Implementing stats here
        Stats();
        SpecBonus();

        //calc after bonus
        BaseStrength = stats[0] + StrengthBonus;
        BaseStamina = stats[1] + StaminaBonus;
        BaseIntellect = stats[2] + IntellectBonus;
        BaseAgility = stats[3] + AgilityBonus;
        BaseHaste = stats[4] + hasteBonus;
        BaseDefence = stats[5] + DefenceBonus;
        BaseCritchance = stats[6] + CritchanceBonus;
        BaseEndurance = stats[7] + EnduranceBonus;
        BaseResistance = stats[8] + ResistanceBonus;
        BaseMemory = stats[9] + MemoryBonus;

    }

    void SpecBonus()
    {
        //Bonuses: +15 % to speed(movement & attackspeed). 10 % to energy regen
        //Drawbacks: +5 % to physical, dark and poison damage taken.
        RunningSpeed *= 1.15f;
        Attackspeed *= 1.15f;
    }
}