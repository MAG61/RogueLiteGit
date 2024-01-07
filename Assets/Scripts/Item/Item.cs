using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public bool isWeapon = false;
    public int cost;

    [TextArea]
    public string description;
    public MAP<BuffTypes.BuffType, float> buffs = new MAP<BuffTypes.BuffType, float>();

    public string GetDisplay()
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine(itemName);
        if (description != null)
        {
            builder.AppendLine(description);
        }
        if (isWeapon)
        {
            builder.AppendLine("Damage: " + GetComponent<Weapon>().damage.ToString());
            builder.AppendLine("Fire rate: " + GetComponent<Weapon>().fireRate.ToString());
            builder.AppendLine("Accuracy: %" + GetComponent<Weapon>().accuracy.ToString());
            builder.AppendLine("Range: " + GetComponent<Weapon>().range.ToString());
        }
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

