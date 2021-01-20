using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpController : MonoBehaviour
{
    [Header("For designers:")]
    [Tooltip("Bullet speed")]
    [Range(3.0f, 15.0f)]
    [SerializeField] private float bulletSpeed = 5.0f;

    private float pausedBulletSpeed;
    private bool isPaused;

    private void Pause()
    {
        if(isPaused)
        {
            bulletSpeed = pausedBulletSpeed;
        }
        else
        {
            pausedBulletSpeed = bulletSpeed;
            bulletSpeed = 0.0f;
        }
        isPaused = !isPaused;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        EventsManager.instance.OnPausePressed += Pause;
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

    private void OnDestroy()
    {
        EventsManager.instance.OnPausePressed -= Pause;
    }
}
