using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyMe : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField]
    private BlastController destructionBlastPrefab;
    [SerializeField] private bool isRespawned = false;
    [Header("For designers:")]
    [Tooltip("Score for destroying.")]
    [SerializeField] private int score = 0;

    private Animator animator;

    protected Text scoreField;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Vehicle")
        {
            if(isRespawned)
            {
                VehicleOptionsController.instance.AddDestroyedObstacle(Instantiate(gameObject, transform.parent));
            }
            if (destructionBlastPrefab != null)
            {
                Instantiate(destructionBlastPrefab, transform.position, destructionBlastPrefab.transform.rotation);
            }
            if (collision.gameObject.tag == "Bullet")
            {
                scoreField.text = (int.Parse(scoreField.text) + score).ToString("000000");
                Destroy(collision.gameObject);
            }
            
            if (animator != null) animator.SetTrigger("Die");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scoreField = GameObject.Find("ScoreCounter").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }
}
