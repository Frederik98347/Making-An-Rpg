namespace PlayerClass
{
    //Class fantasy, all classes has a grimoire flying beside them, helping with casting magic spells / doing all kinds of abilities.
    public enum PlayerDmgTypes
    {
        PHYSICAL = 0,
        SHADOW,
        FROST,
        LIGHTNING,
        DARK,
        FIRE,
        TOXIN
    }

    public enum PlayerClass
    {
        WARRIOR = 0,
        MAGE,
        ROGUE
    }

    public enum WarriorWpnType
    {
        NONE = 0,
        ONE_HANDED_SWORD,
        TWO_HANDED_SWORD,
        DUAL_WIELDED_SWORDS,
        ONE_HANDED_AXE,
        TWO_HANDED_AXE,
        DUAL_WIELDED_AXES,
        ONE_HANDED_MACE,
        TWO_HANDED_MACE,
        DUAL_WIELDED_MACES
    }

    public enum WarriorDmgTypes
    {
        NONE = 0,
        PHYSICAL,
        DARK,
    }

    public enum MageWpnType
    {
        NONE = 0,
        STAFF,
        WAND,
        GRIMOIRE_WAND,
        ONE_HANDED_SWORD,
        ONE_HANDED_MACE,

    }

    public enum MageDmgTypes
    {
        NONE = 0,
        SHADOW,
        FROST,
        LIGHTNING,
        FIRE,
    }

    public enum RogueWpnType
    {
        NONE = 0,
        DAGGERS,
        THROWING_KNIVES,
        ONE_HANDED_SWORD,
        ONE_HANDED_AXE

    }

    public enum RogueDmgTypes
    {
        NONE = 0,
        TOXIN,
        SHADOW,
        PHYSICAL
    }
}