[System.Serializable]
public class Grimoire : Item {
    //set Item type to matching script type
    //grimoire is offhand weapon, so no shield or anything like that
    public Buff buff;

    public enum typeOfGrimoire
    {

        // all the possible book names with the models i got
        BOOKOFEARTH, //Increase tankyness
        BOOKOFDEATH, //
        BOOKOFLIGHTNING, // increase lightning damage
        BOOKOFFIRE, //increase fire damage, add a burn dot effect to all fire spells (on use)
        BOOKOFICE, //increase frost damage + increase slow duration and %
        BOOKOFDARKNESS, // increase dark & physical damage & drain life on hit (onuse)
        BOOKOFCHAOS, // Increase critchance and damage
        BOOKOFILLUSION, // make enemies attack a dummy (onuse effect) & make hp regain possible during combat for x amount of time
        BOOKOFLIGHT, // heal effect
        BOOKOFWIND, //Increase movementspeed by x% & increase attack / casting speed (onuse)
        BOOKOFSUMMONING // being able to summon pets that will attack and tank enemies for you (onuse)
    }

    //spelleffect here
}