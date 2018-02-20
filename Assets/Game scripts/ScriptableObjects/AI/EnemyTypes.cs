namespace EnemyTypes{

    public enum MobRarity
    {
        COMMON,
        RARE,
        EPIC,
        DEMONIC
    }

    public enum MobTypes
    {
        MELEE, // only doing melee attacks
        CASTER, // only casting spells
        HYBRID, //both able to do melee and spells
        ELITE, // Able to do magic damage and melee, while having the max amount of abilities
        BOSS // The strongest type of mob, most damage and coded to be done with high amount prep
    }

    public enum MobDmgTypes
    {
        NONE,
        SHADOW,
        FROST,
        LIGHTNING,
        PHYSICAL,
        DARK,
        FIRE
    }

    public enum MobClass
    {
        NONE,
        WARRIOR,
        MAGE,
        ROGUE
    }

    public enum MobClassTypeWarrior
    {
        NONE,
        TANK,
        BERSKER        
    }

    public enum WarriorWpnType
    {
        NONE,
        ONE_HANDED_SWORD,
        TWO_HANDED_SWORD,
        DUAL_WIELDED_SWORDS,
        GRIMOIRE_ONE_HANDED_SWORD
    }

    public enum WarriorDmgTypes
    {
        NONE,
        PHYSICAL,
        DARK,
    }

    public enum MobClassTypeMage
    {
        NONE,
        CONTROLLER,
        HEAVYSINGLE,
        HEAVYAOE
    }

    public enum MageWpnType
    {
        NONE,
        STAFF,
        WAND,
        GRIMOIRE_WAND
    }

    public enum MageDmgTypes
    {
        NONE,
        SHADOW,
        FROST,
        LIGHTNING,
        FIRE,
    }

    public enum MobClassTypeRogue
    {
        NONE,
        STEALTH,
        SPEED,
        TOXIN
    }

    public enum RogueWpnType
    {
        NONE,
        DAGGERS,
        THROWING_KNIVES,
        GRIMMOIRE_DAGGER
    }

    public enum RogueDmgTypes
    {
        NONE,
        TOXIN,
        DARK,
        SHADOW,
        PHYSICAL
    }
}