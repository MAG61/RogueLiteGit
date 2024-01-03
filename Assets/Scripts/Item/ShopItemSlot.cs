using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemSlot : MonoBehaviour
{
    public Image image;
    public Item currentItem;
    private TextMeshProUGUI desc;
    public bool isLocked = false;
    GameObject toggle;

    private void Start()
    {
        desc = GetComponentInChildren<TextMeshProUGUI>();
        toggle = GetComponentInChildren<Toggle>().gameObject;
    }

    private void Update()
    {
        if (currentItem == null) image.enabled = false;
        else image.enabled = true;
    }

    public void ReloadSlot()
    {
        image.sprite = currentItem.GetComponent<SpriteRenderer>().sprite;
        desc.text = currentItem.GetDisplay();
        toggle.SetActive(true);
    }

    public void ItemBought()
    {
        currentItem = null;
        image.sprite = null;
        desc.text = "";
        toggle.GetComponent<Toggle>().isOn = false;
        toggle.SetActive(false);
    }

    public void SetLock()
    {
        isLocked = !isLocked;
    }
}
