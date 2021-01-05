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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle")
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for(int i=0; i<enemiesCount;i++)
        {
            Instantiate(enemyPrefab, enemyPrefab.transform.position, enemyPrefab.transform.rotation);
            yield return new WaitForSeconds(enemiesDelay);
        }
    }
}
