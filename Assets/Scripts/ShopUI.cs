using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{


    private PlayerInventory inv;

    public ItemSlot[] itemSlots;
    public ShopItemSlot[] shopItems;

    public GameObject[] buyabbles;


    private void Start()
    {
        inv = GameObject.Find("Player").GetComponent<PlayerInventory>();
        itemSlots = GetComponentsInChildren<ItemSlot>();
    }

    public void ReloadItems() { foreach (ItemSlot slot in itemSlots) { slot.ReloadSlot(); } }

    public void ReloadShopItems()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (!shopItems[i].isLocked)
            {
                shopItems[i].currentItem = buyabbles[Random.Range(0, buyabbles.Length)].GetComponent<Item>();
                shopItems[i].ReloadSlot();
            }
        }
    }

    public void Buy(int i)
    {
        if (shopItems[i].currentItem == null) return;
        inv.items.Add(shopItems[i].currentItem);
        if (!SlotsContains(shopItems[i].currentItem)) { AddToSlots(shopItems[i].currentItem); }
        shopItems[i].ItemBought();
        ReloadItems();
        inv.SetStats();
    }

    public bool SlotsContains(Item item)
    {
        foreach (ItemSlot itemslot in itemSlots)
        {
            if (itemslot.currentItem == item) return true;
        }
        return false;
    }

    public void AddToSlots(Item newItem)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].currentItem == null)
            {
                itemSlots[i].currentItem = newItem;
                break;
            }
        }
    }
}
