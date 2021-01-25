using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawnerController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private TextMeshPro scorePrefab;
    [SerializeField] private AudioClip waveClip;
    [Header("For designers:")]
    [Tooltip("Number of enemies in wave")]
    [Range(1, 4)]
    [SerializeField] private int enemiesCount = 2;
    [Tooltip("Time between enemies in wave")]
    [Range(0.2f, 2.0f)]
    [SerializeField] private float enemiesDelay = 0.5f;
    [Tooltip("Spawnpoints for enemies")]
    [SerializeField] private Transform[] spawnPoints;
    [Tooltip("Score for killing all enemies in wave")]
    [SerializeField] private int waveScore = 400;

    private List<Transform> enemyList;
    private Text scoreField;
    private int spawnedEnemiesCount;
    private bool isPaused;
    private AudioSource myAudioSource;

    private void Pause()
    {
        isPaused = !isPaused;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle")
        {
            StartCoroutine(SpawnEnemies());
            myAudioSource.Play();
        }
    }

    private IEnumerator SpawnEnemies()
    {
        GameObject newEnemy;
        int spawnPointIndex = 0;

        for(int i=0; i<enemiesCount;i++)
        {
            if (isPaused) yield return 0;
            newEnemy = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, enemyPrefab.transform.rotation);
            enemyList.Add(newEnemy.GetComponent<Transform>());
            spawnedEnemiesCount++;
            newEnemy.GetComponentInChildren<EnemyController>().AddMySpawner(gameObject);
            if (i == enemiesCount - 1)
            {
                newEnemy.GetComponent<UfoParentController>().SetLast();
            }
            spawnPointIndex++;
            if (spawnPoints.Length <= spawnPointIndex) spawnPointIndex = 0;
            yield return new WaitForSeconds(enemiesDelay);
        }
    }

    private void Start()
    {
        enemyList = new List<Transform>();
        scoreField = GameObject.Find("ScoreCounter").GetComponent<Text>();
        spawnedEnemiesCount = 0;
        isPaused = false;
        EventsManager.instance.OnPausePressed += Pause;
        myAudioSource = gameObject.AddComponent<AudioSource>();
        myAudioSource.clip = waveClip;
    }

    public void SpawnedEnemyKilled(Transform killedEnemy, Vector3 childPosition)
    {
        TextMeshPro scoreText;
        if(enemyList.Count == 1 && spawnedEnemiesCount == enemiesCount) myAudioSource.Stop();
        if (enemyList.Count == 1 && spawnedEnemiesCount == enemiesCount && waveScore > 0)
        {
            scoreField.text = (int.Parse(scoreField.text) + waveScore).ToString("000000");
            scoreText = Instantiate(scorePrefab, childPosition, scorePrefab.transform.rotation);
            scoreText.text = waveScore.ToString();
            Destroy(scoreText.gameObject, 1.5f);
        }
        if(enemyList.Count > 0) enemyList.Remove(killedEnemy);
    }

    private void OnDestroy()
    {
        EventsManager.instance.OnPausePressed -= Pause;
    }
}
