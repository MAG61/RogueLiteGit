using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    private PlayerInventory inv;

    private Image image;
    public Item currentItem;
    private TextMeshProUGUI amountText;

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
        if (currentItem == null) return;
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
}
