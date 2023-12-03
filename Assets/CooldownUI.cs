using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    private Cooldown cooldown;
    [Header("Dash")]
    public Image dashImage;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        cooldown = Cooldown.instance;
        dashImage.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if (cooldown.IsInCooldown(player.GetComponent<CharacterMovement>().dashCooldownKey))
        {
            dashImage.fillAmount = 1 - ((cooldown.cooldowns[player.GetComponent<CharacterMovement>().dashCooldownKey] - Time.time) / player.GetComponent<CharacterMovement>().dashCooldown);
        }
        else
        {
            dashImage.fillAmount = 1;
        }
    }
}
