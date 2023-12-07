using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private Cooldown cooldown;
    public Skill currentSkill;
    void Start()
    {
        cooldown = Cooldown.instance;
        currentSkill = GetComponentInChildren<Skill>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (cooldown.IsInCooldown(currentSkill.skillName))
            {
                GameObject.Find("SkillUI").GetComponent<Animation>().Play("In-Cooldown");
            }
            else
            {
                GameObject.Find("SkillUI").GetComponent<Animation>().Play("Activated");
                currentSkill.Activate();
                cooldown.StartCooldown(currentSkill.skillName, currentSkill.skillCooldown);
            }


        }
    }
}
