using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public static event UnityAction EnemyDead;

    public float maxHealth = 25f;
    private float health = 25f;
    public float speed;
    public GameObject healthBar;
    [SerializeField] private GameObject bloodEffect;

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
        Instantiate<GameObject>(bloodEffect).transform.position = transform.position;
        if (health <= 0)
        {
            EnemyDead?.Invoke();
            Destroy(gameObject);
        }
    }
}
