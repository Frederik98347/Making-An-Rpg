using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

    public RawImage icon;
    Item item;


    public void AddItem (Item newItem)
    {

        item = newItem;

        if (item != null)
        {
            icon.texture = item.Icon;
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
            item.Use();
        }
    }
}
