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
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject damageIndicator;
    [SerializeField] private GameObject coin;

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

        GameObject dmgText = Instantiate<GameObject>(damageIndicator, transform.position, Quaternion.identity, null);
        dmgText.GetComponent<TMPro.TextMeshPro>().text = (dmg).ToString();

        if (health <= 0)
        {
            Instantiate<GameObject>(coin, transform.position, Quaternion.identity, null); 
            EnemyDead?.Invoke();
            DestroyEnd();
        }
    }

    public void DestroyEnd()
    {

        GameObject effect = Instantiate<GameObject>(deathEffect);
        effect.transform.position = transform.position;
#pragma warning disable CS0618 // Tür veya üye artýk kullanýlmýyor
        effect.GetComponentInChildren<ParticleSystem>().startColor = GetComponentInChildren<SpriteRenderer>().color;
#pragma warning restore CS0618 // Tür veya üye artýk kullanýlmýyor
        Destroy(gameObject);
    }
}
