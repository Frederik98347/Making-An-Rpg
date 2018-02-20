using UnityEngine;
[System.Serializable]
public class Item {
    [SerializeField] Icon icon;
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] ItemType itemType;

    public Icon Thumbnail
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
}