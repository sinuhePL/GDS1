using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastLevelController : MonoBehaviour
{
    private RectTransform progressIndicator;
    private float conversionFactor;
    private float startingXShift;
    // Start is called before the first frame update
    void Start()
    {
        progressIndicator = GameObject.Find("Position_indicator").GetComponent<RectTransform>();
        conversionFactor = (195.0f - 117.0f) / transform.position.x;
        startingXShift = 0.0f;
        if (SceneManager.GetActiveScene().name == "Chapter2") startingXShift = 78.0f;
        else if (SceneManager.GetActiveScene().name == "Chapter3") startingXShift = 156.0f;
    }

    // Update is called once per frame
    void Update()
    {
        progressIndicator.anchoredPosition3D = new Vector3(-1.0f * conversionFactor * transform.position.x - 117.0f + startingXShift, progressIndicator.anchoredPosition3D.y, progressIndicator.anchoredPosition3D.z);
    }
}
