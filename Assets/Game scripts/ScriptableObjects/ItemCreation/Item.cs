using UnityEngine;

[CreateAssetMenu(fileName = "Create Item", menuName = "RpgTools/Inventory/Item")]
public class Item : ScriptableObject{
    public bool isStackable = false;
    public int StackSize = 0;
    [SerializeField] Texture2D image;
    [SerializeField] string description;
    [SerializeField] ItemType itemType;
    new string name;

    // setting up
    public Armor.TypeOfArmor armorType;
    public Weapon.TypeOfWeapon weaponType;
    public Consumable.TypeOfPotion potionType;
    public Grimoire.typeOfGrimoire grimoireType;
    public Jewelry.TypeOfJewelry jewlryType;
    public ItemRarity Rarity;

    public enum ItemRarity
    {
        COMMON = 0, // common gear
        RARE, //good gear
        EPIC, // great gear
        DEMONIC //Best gear & = legendary status
    }

    public virtual void Use()
    {
        //use Item
        Debug.Log("Using " + ItemName);
    }

    public ItemType TypeOfItem
    {
        get
        {
            return itemType;
        }

        set
        {
            itemType = value;
        }
    }

    public string ItemName
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Texture2D Image
    {
        get
        {
            return image;
        }

        set
        {
            image = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }
}