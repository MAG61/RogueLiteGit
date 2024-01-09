using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BuffTypes : MonoBehaviour
{
    public enum BuffType
    {
        [Description("Maximum health: ")]
        health,
        [Description("Speed: % ")]
        speedPercent,
        [Description("Damage: % ")]
        damagePercent,
        [Description("Attack speed: % ")]
        attackSpeedPercent
    }

    public static string GetDescription(BuffType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}
