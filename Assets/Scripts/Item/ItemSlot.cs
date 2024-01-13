using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private PlayerInventory inv;

    private Image image;
    public Item currentItem;
    private TextMeshProUGUI amountText;

    [SerializeField] private CraftingSystem crafting;

    private void Start()
    {
        inv = GameObject.Find("Player").GetComponent<PlayerInventory>();
        image = GetComponent<Image>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
        if (amountText != null) amountText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (currentItem == null) image.enabled = false;
        else image.enabled = true;
    }

    public void ReloadSlot()
    {
        if (currentItem != null)
        {
            if (!inv.items.Contains(currentItem) && !currentItem.isWeapon)
            {
                currentItem = null;
                GetComponentInParent<ShopUI>().SlideInventory(this);
            }
        }

        if (currentItem == null)
        {
            image.sprite = null;
            return;
        }
        image.sprite = currentItem.GetComponent<SpriteRenderer>().sprite;
        int amount = 0;

        foreach (Item item in inv.items)
        {
            if (item.Equals(currentItem)) amount++;
        }

        if (amount > 1)
        {
            amountText.gameObject.SetActive(true);
            amountText.text = amount.ToString();
        }
        else
        {
            if (amountText != null) amountText.gameObject.SetActive(false);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!crafting.gameObject.activeSelf) return;

        crafting.InvToCrafting(currentItem);
    }
}
