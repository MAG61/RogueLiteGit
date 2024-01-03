using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapons : MonoBehaviour
{
    public Weapon currentWeapon;
    private PlayerStats stats;

    private void Start()
    {
        stats = GetComponentInParent<PlayerStats>();
    }

    void Update()
    {
        if (currentWeapon == null) return;
        if (currentWeapon.transform.position != transform.position) currentWeapon.transform.position = transform.position;

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies == null) return;

        Enemy nearestEnemy = null;
        foreach(Enemy e in enemies)
        {
            if (nearestEnemy != null)
            {
                if (Vector2.Distance(e.transform.position, transform.position) < Vector2.Distance(nearestEnemy.transform.position, transform.position)) nearestEnemy = e;
            }
            else
            {
                nearestEnemy = e;
            }
        }

        if (nearestEnemy == null) return;
        if (Vector2.Distance(nearestEnemy.transform.position, transform.position) > currentWeapon.range * 1.5) return;


        Vector3 lookDir = (nearestEnemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        currentWeapon.transform.eulerAngles = new Vector3(0, 0, angle);
        if (angle > 90 || angle < -90)
        {
            currentWeapon.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            currentWeapon.transform.localScale = new Vector3(1, 1, 1);
        }
        if (Vector2.Distance(nearestEnemy.transform.position, transform.position) > currentWeapon.range) return;

        if (currentWeapon.canFire) currentWeapon.Shoot(stats.AttackSpeedPercentage, stats.DamagePercentage);
    }



}
