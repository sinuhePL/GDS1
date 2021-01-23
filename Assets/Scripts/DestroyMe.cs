using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyMe : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField]
    private ParticleSystem destructionBlastPrefab;
    [Header("For designers:")]
    [Tooltip("Score for destroying.")]
    [SerializeField] private int score = 0;

    private Animator animator;

    protected Text scoreField;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Vehicle")
        {
            if (destructionBlastPrefab != null)
            {
                ParticleSystem myParticleSystem;
                myParticleSystem = Instantiate(destructionBlastPrefab, transform.position, destructionBlastPrefab.transform.rotation);
                if (!myParticleSystem.IsAlive()) Destroy(myParticleSystem.gameObject);
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
