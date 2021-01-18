using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField]
    private ParticleSystem destructionBlastPrefab;
    [Header("For designers:")]
    [Tooltip("Moon gravity constant.")]
    [Range(2.0f, 8.0f)]
    [SerializeField] private float moonAccelerationConstant = 3.0f;

    private float speed = 0.0f;
    private Vector3 destination;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem myParticleSystem;

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Ground")
        {
            myParticleSystem = Instantiate(destructionBlastPrefab, transform.position, destructionBlastPrefab.transform.rotation);
            myParticleSystem.transform.SetParent(collision.gameObject.transform);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        destination = VehicleOptionsController.instance.dropZones[Random.Range(0, VehicleOptionsController.instance.dropZones.Length)].position;
    }

    // Update is called once per frame
    void Update()
    {
        float step;

        speed += moonAccelerationConstant * Time.deltaTime;
        step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }
}
