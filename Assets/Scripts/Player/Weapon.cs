using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [Space(25)]
    [Header("Weapon Stats")]
    [Range(0.1f, 20f)]
    public float fireRate;
    public float damage = 5f;
    [Range(0, 100f)]
    public float accuracy = 80;
    [Range(0,10f)]
    public float range;

    public bool canFire = true;
    public Transform muzzle;
    public GameObject bullet;

    public Color bulletColor = new Color(1,1,1);

    public void Shoot(float attackSpeedPer, float damagePercentage)
    {
        if (!canFire) return;

        GameObject newBullet = Instantiate<GameObject>(bullet);
        newBullet.transform.position = muzzle.position;
        newBullet.transform.rotation = transform.rotation;
        newBullet.transform.Rotate(0, 0, transform.rotation.z + (Random.Range(-30 + 30 * (accuracy / 100), 30 - 30 * (accuracy / 100))));
        newBullet.GetComponent<Bullet>().damage = damage * (damagePercentage / 100);
        newBullet.GetComponent<SpriteRenderer>().color = bulletColor;
        newBullet.GetComponent<TrailRenderer>().startColor = bulletColor;

        canFire = false;
        StartCoroutine(reLoad(attackSpeedPer /100));
    }

    IEnumerator reLoad(float fireRateMultiplyer)
    {
        yield return new WaitForSeconds(1 / (fireRate * fireRateMultiplyer));
        canFire = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
