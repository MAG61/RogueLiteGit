using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class PlayerInventory : MonoBehaviour
{
    public int coins;
    public float health, attackspeed, damage, speedPercantage, attackspeedPercentage, damagePercantage;

    public List<Item> items;

    private PlayerStats stats;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
        SetStats();
    }

    public void SetStats()
    {
        float nHealth = 0f, nAttackspeed = 0f, nDamage = 0f, nSpeedPer = 0f, nAttackspeedPer = 0f, nDamagePer = 0f;

        foreach (Item item in items)
        {
            foreach (BuffTypes.BuffType buff in item.buffs.KeysList)
            {
                switch (buff)
                {
                    case BuffTypes.BuffType.attackSpeed:
                        nAttackspeed += item.buffs.GetValue(buff);
                        break;

                    case BuffTypes.BuffType.attackSpeedPercent:
                        nAttackspeedPer += item.buffs.GetValue(buff);
                        break;

                    case BuffTypes.BuffType.damage:
                        nDamage += item.buffs.GetValue(buff);
                        break;

                    case BuffTypes.BuffType.damagePercent:
                        nDamagePer += item.buffs.GetValue(buff);
                        break;

                    case BuffTypes.BuffType.health:
                        nHealth += item.buffs.GetValue(buff);
                        break;

                    case BuffTypes.BuffType.speedPercent:
                        nSpeedPer += item.buffs.GetValue(buff);
                        break;

                }
            }
        }

        health = nHealth;
        speedPercantage = nSpeedPer;
        damage = nDamage;
        damagePercantage = nDamagePer;
        attackspeed = nAttackspeed;
        attackspeedPercentage = nAttackspeedPer;

        stats.CountStats();
    }
}
