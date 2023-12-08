using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Collider2D[] inRadius = null;
    public float explotionRadius = 5;
    public float force = 5;
    public float damage = 30;

    [SerializeField] private ParticleSystem explotionEffect;


    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * 5;
        StartCoroutine(Explode());
    }

    void explode()
    {
        inRadius = Physics2D.OverlapCircleAll(transform.position, explotionRadius);

        foreach (Collider2D c in inRadius)
        {
            Rigidbody2D crb = c.GetComponent<Rigidbody2D>();
            if (crb != null && c.tag != "Bullet")
            {
                Vector2 distanceVector = c.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explotionMultipyer = force / distanceVector.magnitude;
                    crb.AddForce(distanceVector.normalized * explotionMultipyer);
                    if (c.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.GetDmg(damage / distanceVector.magnitude);
                    }
                }
            }
        }

    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1.5f);
        explode();
        GetComponent<SpriteRenderer>().enabled = false;
        explotionEffect.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explotionRadius);
    }
}
