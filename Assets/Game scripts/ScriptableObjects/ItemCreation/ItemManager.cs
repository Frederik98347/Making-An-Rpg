﻿using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    #region singleton
    public static ItemManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than one instance of inventory");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> itemList = new List<Item>();

    public bool Add (Item item)
    {

        if (itemList.Count >= space)
        {
            Debug.Log("Not enough Room in inventory.");
            return false;
        }

        itemList.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove (Item item)
    {
        itemList.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}