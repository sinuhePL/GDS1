using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastLevelController : MonoBehaviour
{
    private RectTransform progressIndicator;
    private float conversionFactor;
    private float startingXPosition;
    // Start is called before the first frame update
    void Start()
    {
        progressIndicator = GameObject.Find("Position_indicator").GetComponent<RectTransform>();
        conversionFactor = (195.0f - 117.0f) / transform.position.x;
        startingXPosition = 0.0f;
        if (SceneManager.GetActiveScene().name == "Chapter2") startingXPosition = -117.0f;
        else if (SceneManager.GetActiveScene().name == "Chapter3") startingXPosition = -39.0f;
    }

    // Update is called once per frame
    void Update()
    {
        progressIndicator.anchoredPosition3D = new Vector3(-1.0f * conversionFactor * transform.position.x - 117.0f + startingXPosition, progressIndicator.anchoredPosition3D.y, progressIndicator.anchoredPosition3D.z);
    }
}
