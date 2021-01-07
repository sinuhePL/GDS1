using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Technical:")]
    [Tooltip("Position where animation starts")]
    [SerializeField]
    public Vector3 animationStartPosition = new Vector3(-3.0f, 7.86f, 0.0f);
    [SerializeField]
    public GameObject bombPrefab;
    public Transform bombSpawnPoint;

    private Animator myAnimator;
    private const float speed = 10.0f;

    private IEnumerator MoveToPosition()
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

    private void DropBomb()
    {
        if(Random.Range(0.0f, 1.0f) < 0.5f)
        {
            Instantiate(bombPrefab, bombSpawnPoint.transform.position, bombPrefab.transform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        StartCoroutine(MoveToPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
