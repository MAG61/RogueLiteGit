using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{

    void Start()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(destroy());
    }

IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
