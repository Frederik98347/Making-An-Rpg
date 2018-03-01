using UnityEngine.UI;
using UnityEngine;


public class InventorySlot : MonoBehaviour {

    public RawImage icon;
    public int amount = 0;
    Item item;


    public void AddItem (Item newItem)
    {

        item = newItem;
        if (item.isStackable)
        {
            //set text = amount
            //check if item is already in that slot
            amount += 1;
        }

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
            if (item.isStackable)
            {
                amount -= 1;
                item.Use();
            }

            item.Use();
        }
    }

   /* public bool CheckIfitemIsInInventory (ItemManager item)
    {
        for (int i = 0; i < item.items.Count)
        {
            if (item.items[i].ItemName == item.name)
            {
                return true;
            }
        }
        return false;
    }*/
}
