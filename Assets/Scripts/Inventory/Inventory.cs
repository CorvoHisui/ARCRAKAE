using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event EventHandler OnItemListChange;
    private List<Item> itemList;
    private Action<Item> useItemAction;
    BaseDatos baseDatos = new BaseDatos();

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
        Debug.Log(itemList.Count);

    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool ItemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    ItemAlreadyInInventory = true;
                    baseDatos.SumarItem(item);
                }
            }
            if (!ItemAlreadyInInventory)
            {
                baseDatos.añadirItem(item);
                itemList.Add(item);
            }
        }
        else
            itemList.Add(item);

        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item ItemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                   
                    ItemInInventory = inventoryItem;
                    baseDatos.RestarItem(item);
                }
            }
            if (ItemInInventory != null && ItemInInventory.amount <= 0)
            {
                itemList.Remove(ItemInInventory);
                baseDatos.EliminarObjeto(item);
            }
        }
        else
            itemList.Remove(item);

        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }
    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
