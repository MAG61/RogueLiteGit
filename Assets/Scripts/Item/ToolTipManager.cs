using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    public Canvas parentCanvas;
    public Transform toolTipTransform;
    public static ToolTipManager instance;
    public TextMeshProUGUI text;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out movePos);

        toolTipTransform.position = parentCanvas.transform.TransformPoint(movePos);
    }

    public void Show(string text)
    {
        this.text.text = text;
        toolTipTransform.gameObject.SetActive(true);
    }

    public void Hide()
    {
        toolTipTransform.gameObject.SetActive(false);
    }
}
