using UnityEngine.UI;
using UnityEngine;


public class InventorySlot : MonoBehaviour {

    public RawImage icon;
    public int amount = 0;
    public Item item;
    ItemManager itemManger;


    public void AddItem (Item newItem, int amountToAdd)
    {

        item = newItem;

        if (item != null)
        {
            icon.texture = item.Image;
            icon.enabled = true;
        }
    }

    public void ClearSlot()
    {
        if (item != null)
        {
            item = null;
            icon.texture = null;
            icon.enabled = false;
        }
    }

    public void UseItem ()
    {
        if (item != null)
        {
            if (item.isStackable && CheckIfitemIsInInventory(itemManger))
            {
                for (int i = 0; i < itemManger.items.Count; i++)
                {
                    if (itemManger.items[i].ItemName == this.item.name)
                    {
                        //if this item[i].name = this exact object's name
                        icon.GetComponentInChildren<TMPro.TextMeshPro>().enabled = true;
                        string text = icon.GetComponentInChildren<TMPro.TextMeshPro>().text;
                        amount -= 1;
                        text = amount.ToString();
                        item.Use();
                        break;
                    }
                }
            }

            item.Use();
        }
    }

    public bool CheckIfitemIsInInventory (ItemManager item)
    {
        if (item != null)
        {
            for (int i = 0; i < item.items.Count; i++)
            {
                if (item.items[i].ItemName == this.item.name)
                {
                    //if this item[i].name = this exact object's name
                    return true;
                }
            }
        }

        return false; 
    }
}
