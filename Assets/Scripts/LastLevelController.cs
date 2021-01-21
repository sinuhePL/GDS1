using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastLevelController : MonoBehaviour
{
    private RectTransform progressBar;
    private float conversionFactor;
    private Vector3 barStartingPosition;
    private float startingLength;
    // Start is called before the first frame update
    void Start()
    {
        progressBar = GameObject.Find("Progress Bar").GetComponent<RectTransform>();
        conversionFactor = 227.0f / transform.position.x;
        barStartingPosition = progressBar.transform.position;
        startingLength = 0.0f;
        if (SceneManager.GetActiveScene().name == "Chapter2") startingLength = 228.0f;
        else if (SceneManager.GetActiveScene().name == "Chapter3") startingLength = 456.0f;
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.sizeDelta = new Vector2(-1.0f * conversionFactor * transform.position.x + 227.0f + startingLength, 20.0f);
        progressBar.transform.position = new Vector3(barStartingPosition.x + (-1.0f * conversionFactor * transform.position.x + 227.0f) / 2, barStartingPosition.y, barStartingPosition.z);
    }
}
