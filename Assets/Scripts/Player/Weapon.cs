using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Range(0.1f, 15)]
    public float fireRate;
    public float damage = 5f;
    public Transform muzzle;
    public bool canFire = true;
    [Range(0, 100)]
    public float accuracy = 80;

    public GameObject bullet;

    public void Shoot()
    {
        if (!canFire) return;

        GameObject newBullet = Instantiate<GameObject>(bullet);
        newBullet.transform.position = muzzle.position;
        newBullet.transform.rotation = transform.rotation;
        //newBullet.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + (Random.Range(-15 + 15*(accuracy/100), 15 - 15 * (accuracy / 100))));

        newBullet.transform.Rotate(0, 0, transform.rotation.z + (Random.Range(-15 + 15 * (accuracy / 100), 15 - 15 * (accuracy / 100))));
        //newBullet.transform.eulerAngles = new Vector3(0, 0, transform.rotation.z + (Random.Range(-15f, 15f) / (accuracy/ 100)));
        //Debug.Log(new Vector3(0, 0, transform.rotation.z + (Random.Range(0f, 5f) / (100 / accuracy))));
        newBullet.GetComponent<Bullet>().damage = damage;

        canFire = false;
        StartCoroutine(reLoad());
    }

    IEnumerator reLoad()
    {
        yield return new WaitForSeconds(1 / fireRate);
        canFire = true;
    }
}
