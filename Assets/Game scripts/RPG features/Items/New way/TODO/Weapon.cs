[System.Serializable]
public class Weapon : Item {

//set Item type to matching script type
    public enum TypeOfWeapon
    {
        NONE = 0,
        AWE,
        BOW,
        DAGGER,
        SWORD,
        SHIELD,
        WAND,
        STAFF
    }
}
