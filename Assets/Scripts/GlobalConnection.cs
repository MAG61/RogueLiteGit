using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConnection : MonoBehaviour
{
    public static GlobalConnection instance;
    [Space]
    [Space]
    public static PlayerInventory playerInventory;
    public static PlayerStats playerStats;
    public static ShopUI shopUI;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        if (instance != this) Destroy(this);

        playerInventory = FindObjectOfType<PlayerInventory>();
        playerStats = FindObjectOfType<PlayerStats>();
        shopUI = FindObjectOfType<ShopUI>();
    }
}
