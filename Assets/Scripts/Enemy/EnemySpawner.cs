using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    public float baseCooldown = 2f;
    public int baseNumberOfEnemies = 3;
    private float cooldown;
    private int numberOfEnemies;
    public GameObject enemyPrefab;
    public bool state;

    private void Start()
    {
        numberOfEnemies = baseNumberOfEnemies;
        cooldown = baseCooldown;
    }

    public IEnumerator Spawn()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate<GameObject>(enemyPrefab);
            newEnemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }

        yield return new WaitForSeconds(cooldown);
        StartCoroutine(Spawn());
    }

    public void SetCoolDown(float newc) { cooldown = newc; }
    public void SetNumberOfEnemies(int newc) { numberOfEnemies = newc; }
}
