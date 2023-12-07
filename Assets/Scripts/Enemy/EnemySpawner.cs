using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    public float cooldown = 2f;
    public int numberOfEnemies = 3;
    public GameObject enemyPrefab;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate<GameObject>(enemyPrefab);
            newEnemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }

        yield return new WaitForSeconds(cooldown);
        StartCoroutine(Spawn());
    }
}
