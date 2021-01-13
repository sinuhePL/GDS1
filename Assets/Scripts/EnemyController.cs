using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{
    [Header("Technical:")]
    [Tooltip("Position where animation starts")]
    [SerializeField]
    protected Vector3 animationStartPosition = new Vector3(-3.0f, 7.86f, 0.0f);
    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private Transform bombSpawnPoint;
    [Header("For designers:")]
    [Tooltip("Probability of dropping bomb in drop point.")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float bombProbability = 0.5f;

    protected Animator myAnimator;
    protected GameObject mySpawner;
    protected const float speed = 10.0f;

    protected virtual IEnumerator MoveToPosition()
    {
        float step;
        
        while(Vector3.Distance(transform.position, animationStartPosition) > 0.001f)
        {
            step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, animationStartPosition, step);
            yield return 0;
        }
        myAnimator.enabled = true;
    }

    protected void DropBomb()
    {
        if(Random.Range(0.0f, 1.0f) < bombProbability)
        {
            Instantiate(bombPrefab, bombSpawnPoint.transform.position, bombPrefab.transform.rotation);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        StartCoroutine(MoveToPosition());
    }

    public void AddMySpawner(GameObject spawner)
    {
        mySpawner = spawner;
    }

    private void OnDestroy()
    {
        mySpawner.GetComponent<EnemySpawnerController>().SpawnedEnemyKilled(gameObject);
    }
}
