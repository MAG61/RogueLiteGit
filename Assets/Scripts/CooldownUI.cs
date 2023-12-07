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
    [Header("Skill")]
    public Image skillImage;

    void Start()
    {
        player = GameObject.Find("Player");
        cooldown = Cooldown.instance;
        dashImage.fillAmount = 1;
        skillImage.fillAmount = 1;
        // skillImage.sprite = player.GetComponent<SkillManager>().currentSkill.skillSprite;
        Image[] ims = skillImage.GetComponentsInChildren<Image>();
        foreach(Image im in ims) im.sprite = player.GetComponent<SkillManager>().currentSkill.skillSprite;
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        Skill();
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

    private void Skill()
    {
        if (cooldown.IsInCooldown(player.GetComponent<SkillManager>().currentSkill.skillName))
        {
            skillImage.fillAmount = 1 - ((cooldown.cooldowns[player.GetComponent<SkillManager>().currentSkill.skillName] - Time.time) / player.GetComponent<SkillManager>().currentSkill.skillCooldown);
        }
        else
        {
            skillImage.fillAmount = 1;
        }
    }
}
