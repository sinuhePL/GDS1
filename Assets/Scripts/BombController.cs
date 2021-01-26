using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField]
    private BlastController destructionBlastPrefab;
    [Header("For designers:")]
    [Tooltip("Moon gravity constant.")]
    [Range(2.0f, 8.0f)]
    [SerializeField] private float moonAccelerationConstant = 3.0f;

    private Animator animator;
    private AudioSource myAudioSource;

    protected float speed = 0.0f;
    protected Vector3 destination;
    protected float pausedSpeed;
    protected bool isPaused;

    protected void Pause()
    {
        if (isPaused)
        {
            speed = pausedSpeed;
        }
        else
        {
            pausedSpeed = speed;
            speed = 0.0f;
        }
        isPaused = !isPaused;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Explode();
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Vehicle")
        {
            myAudioSource.Play();
            Explode();
        }
    }

    private void Explode()
    {
        if (destructionBlastPrefab != null)
        {
            BlastController blastController = Instantiate(destructionBlastPrefab, transform.position, destructionBlastPrefab.transform.rotation);
            Destroy(gameObject);
        }
        else if (animator != null)
            animator.SetTrigger("Explode");
        else
        {
            Destroy(gameObject);
            Debug.LogWarning(gameObject.name + " has no explosion effect!");
        }
    }

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        destination = VehicleOptionsController.instance.dropZones[Random.Range(0, VehicleOptionsController.instance.dropZones.Length)].position;
        isPaused = false;
        EventsManager.instance.OnPausePressed += Pause;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float step;

        if (!isPaused)
        {
            speed += moonAccelerationConstant * Time.deltaTime;
            step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }
    }

    protected void OnDestroy()
    {
        EventsManager.instance.OnPausePressed -= Pause;
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
