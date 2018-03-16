using PlayerClass;

public class BaseWarriorClass : BaseCharacterClass
{

    // all generic enemy data for War class
    public WarriorWpnType wpnType;
    public WarriorDmgTypes dmgType;

    //warrior generic abilities
    public Spell[] warriorAbilities;

    //stat bonuses
    int StaminaBonus = 2;
    int StrengthBonus = 2;
    int IntellectBonus = -2;
    int AgilityBonus = -2;
    int CritchanceBonus = -2;
    int DefenceBonus = 2;
    int hasteBonus = -1;
    int ResistanceBonus = 2;
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

    public void WarriorClass()
    {
        // can take more damage, casts spells and able to avoid some damage.
        CharacterClassName = "Warrior";
        CharacterClassDescription = "Brutal Heavy hitter whos also able to withstand damage toe to toe with hardhitting enemies. Expert in close quarters, but also suffers from low intellect and agility";
        ClassBonus = "Bonuses: +15% to physical and dark damage. 10% to defence. Drawbacks: +5 % slowduration and - 10 % energy.";
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
        //if damage = to dark or physical increase by 15%
        //+10% defence
        // -10% energy and +5% slow duration

    }
}