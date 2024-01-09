using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int coins;
    [SerializeField] private TextMeshProUGUI currencyText;

    public float speedPercantage, attackspeedPercentage, damagePercantage;

    public List<Item> items;

    private PlayerStats stats;

    Collider2D[] inRadius = null;
    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        inRadius = Physics2D.OverlapCircleAll(transform.position, 3);
        foreach (Collider2D c in inRadius) if (c.transform.CompareTag("Coin")) { c.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(c.transform.position, transform.position, 0.15f)); }
    }

    public void SetStats()
    {
        float nSpeedPer = 0f, nAttackspeedPer = 0f, nDamagePer = 0f;

        foreach (Item item in items)
        {
            foreach (BuffTypes.BuffType buff in item.buffs.KeysList)
            {
                switch (buff)
                {
                    case BuffTypes.BuffType.attackSpeedPercent:
                        nAttackspeedPer += item.buffs.GetValue(buff);
                        break;

                    case BuffTypes.BuffType.damagePercent:
                        nDamagePer += item.buffs.GetValue(buff);
                        break;

                    case BuffTypes.BuffType.speedPercent:
                        nSpeedPer += item.buffs.GetValue(buff);
                        break;

                }
            }
        }

        speedPercantage = nSpeedPer;
        damagePercantage = nDamagePer;
        attackspeedPercentage = nAttackspeedPer;

        stats.CountStats();
    }

    public void RefreshCurrency()
    {
        currencyText.text = coins.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coins++;
        }
    }
}
