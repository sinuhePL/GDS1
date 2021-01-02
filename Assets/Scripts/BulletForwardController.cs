using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForwardController : MonoBehaviour
{

    [Header("For designers:")]
    [Tooltip("Bullet speed")]
    [Range(1.0f, 8.0f)]
    [SerializeField] private float bulletSpeed = 4.0f;
    [Tooltip("Bullet range")]
    [Range(1.0f, 4.0f)]
    [SerializeField] private float bulletRange = 2.0f;

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    private void MoveBullet()
    {
        transform.Translate(new Vector3(Time.deltaTime * bulletSpeed, 0.0f, 0.0f));
        if (transform.position.x - startingPosition.x > bulletRange) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }
}
