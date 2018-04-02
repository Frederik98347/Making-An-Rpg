using RpgTools.PlayerClass;

public class BaseMageClass : BaseCharacterClass{
    // all generic enemy data for War class
    public PlayerDmgTypes dmgTypes;

    //warrior generic abilities
    public Spell[] MageAbilities;

    //stat bonuses
    int StaminaBonus = -2;
    int StrengthBonus = -3;
    int IntellectBonus = 3;
    int AgilityBonus = -1;
    int CritchanceBonus = 1;
    int DefenceBonus = -2;
    int hasteBonus = 2;
    int ResistanceBonus = 1;
    int EnduranceBonus = 0;
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

    public void MageClass()
    {
        // can take more damage, casts spells and able to avoid some damage.
        CharacterClassName = "Mage";
        CharacterClassDescription = "A Wizard who deals a lot of damage at range, but takes alot of physical damage in return";
        ClassBonus = "Bonuses: +10% to all types of magic damage and 10% more energy," +
            " Drawbacks: -15 % to all non magical damage done.";
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
        //Bonuses: +10 % to all types of magic damage and 10 % more energy
        //Drawbacks: -15 % to all non magical damage done.

    }
}