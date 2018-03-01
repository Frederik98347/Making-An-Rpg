namespace EnemyTypes{

    public enum MobRarity
    {
        COMMON = 0,
        RARE,
        EPIC,
        DEMONIC
    }

    public enum MobTypes
    {
        MELEE = 0, // only doing melee attacks
        CASTER, // only casting spells
        HYBRID, //both able to do melee and spells
        ELITE, // Able to do magic damage and melee, while having the max amount of abilities
        BOSS // The strongest type of mob, most damage and coded to be done with high amount prep
    }

    public enum MobDmgTypes
    {
        NONE = 0,
        SHADOW,
        FROST,
        LIGHTNING,
        PHYSICAL,
        DARK,
        FIRE
    }

    public enum MobClass
    {
        NONE = 0,
        WARRIOR,
        MAGE,
        ROGUE
    }

    public enum MobClassTypeWarrior
    {
        NONE = 0,
        TANK,
        BERSKER        
    }

    public enum WarriorWpnType
    {
        NONE = 0,
        ONE_HANDED_SWORD,
        TWO_HANDED_SWORD,
        DUAL_WIELDED_SWORDS,
        GRIMOIRE_ONE_HANDED_SWORD
    }

    public enum WarriorDmgTypes
    {
        NONE = 0,
        PHYSICAL,
        DARK,
    }

    public enum MobClassTypeMage
    {
        NONE = 0,
        CONTROLLER,
        HEAVYSINGLE,
        HEAVYAOE
    }

    public enum MageWpnType
    {
        NONE = 0,
        STAFF,
        WAND,
        GRIMOIRE_WAND
    }

    public enum MageDmgTypes
    {
        NONE = 0,
        SHADOW,
        FROST,
        LIGHTNING,
        FIRE,
    }

    public enum MobClassTypeRogue
    {
        NONE = 0,
        STEALTH,
        SPEED,
        TOXIN
    }

    public enum RogueWpnType
    {
        NONE = 0,
        DAGGERS,
        THROWING_KNIVES,
        GRIMMOIRE_DAGGER
    }

    public enum RogueDmgTypes
    {
        NONE = 0,
        TOXIN,
        DARK,
        SHADOW,
        PHYSICAL
    }
}