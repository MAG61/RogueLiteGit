using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public List<string> recipes;
    public List<GameObject> outputs;
    [SerializeField] private PlayerInventory inv;
    [SerializeField] private ShopUI shop;
    [SerializeField] private CraftingSlot[] slots;
    [SerializeField] private CraftingSlot outputSlot;

    void Start()
    {

    }

    void Update()
    {

    }

    public void CheckForCrafting()
    {
        string currentCraft = "";
        foreach (CraftingSlot slot in slots) { if (slot.currentItem != null) { currentCraft += slot.currentItem.itemName; } }
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i] == currentCraft)
            {
                outputSlot.currentItem = outputs[i].GetComponent<Item>();
                outputSlot.ReloadSlot();
            }
        }
    }

    public void InvToCrafting(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].currentItem == null)
            {
                slots[i].currentItem = item;
                shop.GoCraft(item);
                break;
            }
        }

        foreach (CraftingSlot slot in slots) slot.ReloadSlot();
        CheckForCrafting();
    }

    public void CraftingToInv(int index)
    {

        slots[index].currentItem.gameObject.SetActive(true);

        shop.GoInv(slots[index].currentItem);


        slots[index].currentItem = null;

        foreach (CraftingSlot slot in slots) slot.ReloadSlot();
        CheckForCrafting();
    }

    public void OutputClicked()
    {
        if (slots[0].currentItem.isWeapon)
        {
            Destroy(slots[0].currentItem.gameObject);
            slots[0].currentItem = null;
        }
        if (slots[1].currentItem.isWeapon)
        {
            Destroy(slots[1].currentItem.gameObject);
            slots[1].currentItem = null;
        }

        shop.GoInv(outputSlot.currentItem, true);

        outputSlot.currentItem = null;
        outputSlot.ReloadSlot();

        foreach (CraftingSlot slot in slots) slot.ReloadSlot();
        CheckForCrafting();
    }
}
