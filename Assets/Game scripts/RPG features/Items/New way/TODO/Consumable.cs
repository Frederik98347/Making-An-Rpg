[System.Serializable]
public class Consumable : Item {
    //set Item type to matching script type
    public bool IsStackable = true;
    public int Stacksize = 20;
    public enum TypeOfPotion
    {
        NONE = 0,
        MANAPOTION,
        HEALTHPOTION
    }
}