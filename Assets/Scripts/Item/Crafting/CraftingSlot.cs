using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CraftingSlot : MonoBehaviour, IPointerClickHandler
{
    public Item currentItem;

    public UnityEvent OnCraftSlotClicked;
    public UnityEvent OnOutputSlotClicked;
    private Image image;

    private bool isOutputSlot;

    private void Start()
    {
        image = GetComponent<Image>();
        if (gameObject.name == "CraftOutput") isOutputSlot = true;
    }
    public void ReloadSlot()
    {
        if (currentItem != null)
        {
            image.sprite = currentItem.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            image.sprite = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            if (isOutputSlot)
            {
                OnOutputSlotClicked?.Invoke();
            }
            else
            {
                OnCraftSlotClicked?.Invoke();
            }
        }
    }
}
