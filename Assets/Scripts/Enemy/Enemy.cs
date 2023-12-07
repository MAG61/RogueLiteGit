using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 25f;
    private float health = 25f;
    public float speed;
    public GameObject healthBar;

    private void Start()
    {
        health = maxHealth;
    }

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
        healthBar.transform.localScale = new Vector3(health / maxHealth, 1, 1);
        if (health <= 0) Destroy(gameObject);
    }
}
