using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string skillName;
    public SkillType skillType;
    public float skillCooldown = 3f;
    public Sprite skillSprite;

    public float skillDamage = 5f;

    [SerializeField] GameObject bombPrefab;

    public enum SkillType{
        Bomb,
    }

    public void Activate()
    {
        if (skillType == SkillType.Bomb)
        {
            Weapon currentWeapon = GetComponentInParent<WeaponManager>().mainWeapon;

            GameObject newBomb = Instantiate<GameObject>(bombPrefab);
            newBomb.transform.position = currentWeapon.muzzle.position;
            newBomb.transform.rotation = currentWeapon.transform.rotation;
            newBomb.GetComponent<Bomb>().damage = skillDamage;
        }
    }
}
