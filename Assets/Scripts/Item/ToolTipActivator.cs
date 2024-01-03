using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<ItemSlot>().currentItem == null) return;
        ToolTipManager.instance.Show(GetComponent<ItemSlot>().currentItem.GetDisplay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManager.instance.Hide();
    }
}
