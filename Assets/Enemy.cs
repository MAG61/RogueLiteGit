using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 25;
    public float speed;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce((GameObject.Find("Player").transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            GetDmg(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }

    public void GetDmg(float dmg)
    {
        health -= dmg;
        if (health <= 0) Destroy(gameObject);
    }
}
