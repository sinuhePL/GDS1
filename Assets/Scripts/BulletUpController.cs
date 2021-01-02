using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpController : MonoBehaviour
{
    [Header("For designers:")]
    [Tooltip("Bullet speed")]
    [Range(3.0f, 10.0f)]
    [SerializeField] private float bulletSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void MoveBullet()
    {
        transform.Translate(new Vector3(0.0f, Time.deltaTime * bulletSpeed, 0.0f));
        if (transform.position.y > 10.0f) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }
}
