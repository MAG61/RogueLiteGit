using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100,100));
        StartCoroutine(destroy());
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
