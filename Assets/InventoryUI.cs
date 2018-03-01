﻿using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    ItemManager inventory;
    public GameObject inventoryUI;
    public Item item;

    InventorySlot[] slots;

	// Use this for initialization
	void Start () {
        inventory = ItemManager.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
	}

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
}