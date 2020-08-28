using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public event EventHandler OnItemListChanged;
    
    public Inventory()
    {
        itemList = new List<Item>();

        //Testing inventory
        /*AddItem(new Item { itemType = Item.ItemType.Heart, amount = 1 }); //adds one Heart to inventory
        AddItem(new Item { itemType = Item.ItemType.Coin, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Gem, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Gem, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Gem, amount = 1 });     
        AddItem(new Item { itemType = Item.ItemType.Barricade, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Coin, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Coin, amount = 100 });*/
        //Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        if (item.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;                    
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty); //Triggers event OnItemListChanged
        //Debug.Log(item.itemType);
    }

    public void RemoveItem(Item item)
    {
        if (item.isStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(item);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty); //Triggers event OnItemListChanged
        //Debug.Log(item.itemType);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public bool isInventoryFull(Item item)
    {
        if (itemList.Count >= 14)
        {
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    if (item.isStackable())
                    {
                        return false;
                    } else if (!item.isStackable())
                    {
                        return true;
                    }
                }
            }
            return true;
        } else
        {
            return false;
        }
    }
}
