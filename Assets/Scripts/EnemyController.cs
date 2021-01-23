using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private Transform bombSpawnPoint;
    [Header("For designers:")]
    [Tooltip("Probability of dropping bomb in drop point.")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float bombProbability = 0.5f;
    [Tooltip("Probability of changing circle.")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float circleChangeProbability = 0.5f;

    protected GameObject mySpawner;
    protected Animator myAnimator;

    protected void DropBomb()
    {
        if(Random.Range(0.0f, 1.0f) < bombProbability)
        {
            Instantiate(bombPrefab, bombSpawnPoint.transform.position, bombPrefab.transform.rotation);
        }
    }

    protected void ChangeLevel()
    {
        GetComponentInParent<UfoParentController>().ChangeCircleLevel();
    }

    public void AddMySpawner(GameObject spawner)
    {
        mySpawner = spawner;
    }

    protected void ChangeCircle()
    {
        if (Random.Range(0.0f, 1.0f) < circleChangeProbability)
        {
            if(myAnimator.GetBool("rightCircle")) myAnimator.SetBool("rightCircle", false);
            else myAnimator.SetBool("rightCircle", true);
        }
    }

    private void OnDestroy()
    {
        EnemySpawnerController esc;
        Transform t;
        if (mySpawner != null)
        {
            esc = mySpawner.GetComponent<EnemySpawnerController>();
            t = GetComponentInParent<Transform>();
            if (t != null) esc.SpawnedEnemyKilled(t, transform.position);
        }
    }

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
}
