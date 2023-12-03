using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cooldown : MonoBehaviour
{
    public Dictionary<string, float> cooldowns;
    public static Cooldown instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        if (instance != this) Destroy(this);
    }

    private void Start()
    {
        cooldowns = new Dictionary<string, float>();
    }
    public bool IsInCooldown(string key)
    {
        if (!cooldowns.ContainsKey(key)) return false;
        else return Time.time <= cooldowns[key];

    }
    public void StartCooldown(string key, float cooldown)
    {
        if (cooldowns.ContainsKey(key))
        {
            cooldowns[key] = Time.time + cooldown;
        }
        else
        {
            cooldowns.Add(key, Time.time + cooldown);
        }
    }


}
