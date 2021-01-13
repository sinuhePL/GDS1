using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour
{
    [Header("For designers:")]
    [Tooltip("Score for jumping over.")]
    [SerializeField] private int score = 0;

    protected Text scoreField;

    // Start is called before the first frame update
    void Start()
    {
        scoreField = GameObject.Find("ScoreCounter").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Vehicle")
        {
            scoreField.text = (int.Parse(scoreField.text) + score).ToString("000000");
        }
    }
}
