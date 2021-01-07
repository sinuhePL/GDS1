using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField] private GameObject enemyPrefab;
    [Header("For designers:")]
    [Tooltip("Number of enemies in wave")]
    [Range(1, 4)]
    [SerializeField] private int enemiesCount = 2;
    [Tooltip("Time between enemies in wave")]
    [Range(0.2f, 2.0f)]
    [SerializeField] private float enemiesDelay = 0.5f;
    [Tooltip("Spawnpoints for enemies")]
    [SerializeField] private Transform[] spawnPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle")
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        GameObject newEnemy;
        int spawnPointIndex = 0;

        for(int i=0; i<enemiesCount;i++)
        {
            newEnemy = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, enemyPrefab.transform.rotation);
            spawnPointIndex++;
            if (spawnPoints.Length <= spawnPointIndex) spawnPointIndex = 0;
            yield return new WaitForSeconds(enemiesDelay);
        }
    }
}
