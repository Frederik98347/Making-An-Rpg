using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Create Item", menuName = "RpgTools/Inventory/Item")]
public class Item : ScriptableObject{
    [SerializeField] Texture2D icon;
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] ItemType itemType;

    public virtual void Use()
    {
        //use Item
        Debug.Log("Using " + name);
    }

    public string Name
    {
        get
        {
            return itemName;
        }

        set
        {
            itemName = value;
        }
    }

    public string Description
    {
        get
        {
            return itemDescription;
        }

        set
        {
            itemDescription = value;
        }
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

    public Texture2D Icon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }
}