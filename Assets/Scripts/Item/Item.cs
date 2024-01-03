using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;

    public MAP<BuffTypes.BuffType, float> buffs = new MAP<BuffTypes.BuffType, float>();

    public string GetDisplay()
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine(itemName);
        builder.AppendLine();
        foreach (BuffTypes.BuffType b in buffs.KeysList)
        {
            builder.Append(BuffTypes.GetDescription(b));
            if (buffs.GetValue(b) < 0)
            {
                builder.AppendLine("<color=#" + ColorUtility.ToHtmlStringRGBA(Color.red) + ">" + buffs.GetValue(b).ToString() + "</color>");
            }
            else
            {
                builder.AppendLine("<color=#" + ColorUtility.ToHtmlStringRGBA(Color.green) + ">+" + buffs.GetValue(b).ToString() + "</color>");
            }
        }
        return builder.ToString();
    }

}

