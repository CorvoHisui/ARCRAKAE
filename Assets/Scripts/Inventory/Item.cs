using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item 
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
        ManaPotion,
        Coin,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
                
            case ItemType.Sword:
                return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion:
                return ItemAssets.Instance.HealthPotionSprite;
            case ItemType.ManaPotion:
                return ItemAssets.Instance.ManaPotionSprite;
            case ItemType.Coin:
                return ItemAssets.Instance.CoinSprite;

        }
    }
    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return 0;
            case ItemType.HealthPotion: return 30;
            case ItemType.ManaPotion: return 100;
            
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
                return true;
            case ItemType.Sword:
                return false;
        }
    }
}
