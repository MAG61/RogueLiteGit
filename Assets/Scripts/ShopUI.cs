using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{


    private PlayerInventory inv;
    private WeaponManager wManager;

    public ItemSlot[] itemSlots;
    public ShopItemSlot[] shopItems;
    public ItemSlot[] weaponSlots;

    public GameObject[] buyabbles;


    private void Start()
    {
        inv = GameObject.Find("Player").GetComponent<PlayerInventory>();
        wManager = GameObject.Find("Player").GetComponent<WeaponManager>();
        itemSlots = GetComponentsInChildren<ItemSlot>();
    }

    public void ReloadItems() { foreach (ItemSlot slot in itemSlots) { slot.ReloadSlot(); } }

    public void ReloadShopItems()
    {
        inv.RefreshCurrency();
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (!shopItems[i].isLocked)
            {
                shopItems[i].currentItem = buyabbles[Random.Range(0, buyabbles.Length)].GetComponent<Item>();
                shopItems[i].ReloadSlot();
            }
        }
    }

    public void ReloadShopItems(int cost)
    {
        if (inv.coins >= cost)
        {
            inv.coins -= cost;
            inv.RefreshCurrency();
            for (int i = 0; i < shopItems.Length; i++)
            {
                if (!shopItems[i].isLocked)
                {
                    shopItems[i].currentItem = buyabbles[Random.Range(0, buyabbles.Length)].GetComponent<Item>();
                    shopItems[i].ReloadSlot();
                }
            }
        }
    }

    public void ReloadWeaponSlots()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (i < wManager.weapons.KeysList.Count)
            {
                weaponSlots[i].currentItem = wManager.weapons.KeysList[i].GetComponent<Item>();
            }
            else
            {
                weaponSlots[i].currentItem = null;
            }
            weaponSlots[i].ReloadSlot();
        }
    }

    public void Buy(int i)
    {
        if (shopItems[i].currentItem == null) return;
        if (inv.coins >= shopItems[i].currentItem.cost)
        {
            inv.coins -= shopItems[i].currentItem.cost;
            inv.RefreshCurrency();

            if (!shopItems[i].currentItem.isWeapon)
            {
                ItemBought(i);
            }
            else
            {
                WeaponBought(i);
            }
        }
    }

    private void ItemBought(int i)
    {
        inv.items.Add(shopItems[i].currentItem);
        if (!SlotsContains(shopItems[i].currentItem)) { AddToSlots(shopItems[i].currentItem); }
        shopItems[i].ItemBought();
        ReloadItems();
        inv.SetStats();
    }

    private void WeaponBought(int i)
    {
        inv.GetComponent<WeaponManager>().AddWeapon(shopItems[i].currentItem);
        shopItems[i].ItemBought();
        ReloadWeaponSlots();
    }

    public void GoInv(Item item, bool isNew = false)
    {
        if (!item.isWeapon)
        {
            ItemGoInv(item);
        }
        else
        {
            WeaponGoInv(item, isNew);
        }
    }

    private void ItemGoInv(Item item)
    {
        inv.items.Add(item);
        if (!SlotsContains(item)) { AddToSlots(item); }
        ReloadItems();
    }

    private void WeaponGoInv(Item item, bool isNew = false)
    {
        if (isNew) inv.GetComponent<WeaponManager>().AddWeapon(item);
        wManager.RefreshWeapons();
        ReloadWeaponSlots();
    }

    public void GoCraft(Item item)
    {
        if (!item.isWeapon)
        {
            ItemGoCraft(item);
        }
        else
        {
            WeaponGoCraft(item);
        }
    }

    private void ItemGoCraft(Item item)
    {
        inv.items.Remove(item);
        ReloadItems();
    }

    private void WeaponGoCraft(Item item)
    {
        inv.GetComponent<WeaponManager>().RemoveWeapon(item);
        wManager.RefreshWeapons();
        ReloadWeaponSlots();
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


    public void SlideInventory(ItemSlot slot)
    {
        int index = 0;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] == slot)
            {
                index = i;
                break;
            }
        }

        for (int i = index; i < itemSlots.Length - 1; i++)
        {
            itemSlots[i].currentItem = itemSlots[i + 1].currentItem;
        }
        itemSlots[itemSlots.Length - 1].currentItem = null;
        ReloadItems();
    }
}
