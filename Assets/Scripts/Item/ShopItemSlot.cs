using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    public Item currentItem;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private TextMeshProUGUI price;
    public bool isLocked = false;
    GameObject toggle;

    private void Start()
    {
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
        price.text = currentItem.cost.ToString();
        price.gameObject.SetActive(true);
        toggle.SetActive(true);
    }

    public void ItemBought()
    {
        currentItem = null;
        image.sprite = null;
        desc.text = "";
        price.gameObject.SetActive(false);
        toggle.GetComponent<Toggle>().isOn = false;
        toggle.SetActive(false);
    }

    public void SetLock()
    {
        isLocked = !isLocked;
    }
}
