using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("For designers:")]
    [Tooltip("Moon gravity constant.")]
    [Range(2.0f, 8.0f)]
    [SerializeField] private float moonAccelerationConstant = 3.0f;

    private float speed = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed -= moonAccelerationConstant * Time.deltaTime;
        transform.Translate(new Vector3(0.0f, Time.deltaTime * speed, 0.0f));
        if (transform.position.y <= -0.8f) Destroy(gameObject);
    }
}
