using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage;
    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
    }

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}


