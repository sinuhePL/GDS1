using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForwardController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField]
    private BlastController destructionBlastPrefab;
    [Header("For designers:")]
    [Tooltip("Bullet speed")]
    [Range(1.0f, 10.0f)]
    [SerializeField] private float bulletSpeed = 4.0f;
    [Tooltip("Bullet range")]
    [Range(1.0f, 4.0f)]
    [SerializeField] private float bulletRange = 2.0f;

    private Vector3 startingPosition;
    private float pausedBulletSpeed;
    private bool isPaused;

    private void Pause()
    {
        if (isPaused)
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
        startingPosition = transform.position;
        isPaused = false;
        EventsManager.instance.OnPausePressed += Pause;
    }

    private void MoveBullet()
    {
        transform.Translate(new Vector3(Time.deltaTime * bulletSpeed, 0.0f, 0.0f));
        if (transform.position.x - startingPosition.x > bulletRange)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }

    private void OnDestroy()
    {
        BlastController blastController;
        //ParticleSystem.MainModule psmain;

        blastController = Instantiate(destructionBlastPrefab, transform.position, destructionBlastPrefab.transform.rotation);
        //psmain = myParticleSystem.main;
        //psmain.startLifetime = 0.2f;
        EventsManager.instance.OnPausePressed -= Pause;
    }
}
