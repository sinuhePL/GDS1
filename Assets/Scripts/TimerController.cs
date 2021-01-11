using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private float timeCounter;
    private Text timerField;
    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0.0f;
        timerField = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        timerField.text = ((int)timeCounter).ToString("000");
    }
}
