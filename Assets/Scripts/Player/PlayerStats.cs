using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerInventory inv;

    [Header("Base Movement Stats")]
    public float baseSpeed;
    public float baseDashSpeed;
    [Space]
    [Space]

    [Header("Base Shooting Stats")]
    public float baseDamagePercentage;
    public float baseAttackSpeedPercentage;
    public float baseHealth;
    [Space]
    [Space]

    [Header("Movement Stats")]
    public float Speed;
    public float DashSpeed;
    [Space]
    [Space]

    [Header("Shooting Stats")]
    public float DamagePercentage;
    public float AttackSpeedPercentage;
    public float Health;

    private void Start()
    {
        inv = GetComponent<PlayerInventory>();
    }

    public void CountStats()
    {
        DamagePercentage = baseDamagePercentage + inv.damagePercantage;
        Speed = baseSpeed * (1 + (inv.speedPercantage / 100));
        AttackSpeedPercentage = baseAttackSpeedPercentage + inv.attackspeedPercentage;
    }
}
