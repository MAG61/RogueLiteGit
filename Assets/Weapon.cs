using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate;
    public float damage = 5f;
    public Transform muzzle;
    public bool canFire = true;

    public GameObject bullet;

    public void Shoot()
    {
        if (!canFire) return;

        GameObject newBullet = Instantiate<GameObject>(bullet);
        newBullet.transform.position = muzzle.position;
        newBullet.transform.rotation = transform.rotation;
        newBullet.GetComponent<Bullet>().damage = damage;

        canFire = false;
        StartCoroutine(reLoad());
    }

    IEnumerator reLoad()
    {
        yield return new WaitForSeconds(1/fireRate);
        canFire = true;
    }
}
