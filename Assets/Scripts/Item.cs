using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Heart,
        Coin,
        Gem,
        Wood,
        Apple,
        Barricade
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Heart:
                return ItemAssets.Instance.heartSprite;
            case ItemType.Coin:
                return ItemAssets.Instance.coinSprite;
            case ItemType.Gem:
                return ItemAssets.Instance.gemSprite;
            case ItemType.Wood:
                return ItemAssets.Instance.woodSprite;
            case ItemType.Apple:
                return ItemAssets.Instance.appleSprite;
            case ItemType.Barricade:
                return ItemAssets.Instance.barricadeSprite;
        }
    }

    public bool isStackable ()
    {
        switch(itemType)
        {
            default:
            case ItemType.Coin:
                return true;
            case ItemType.Gem:
                return true;
            case ItemType.Heart:
                return false;
            case ItemType.Wood:
                return true;
            case ItemType.Apple:
                return false;
            case ItemType.Barricade:
                return false;
        }
    }
}
