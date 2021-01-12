using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private float timeCounter;
    private Text timerField;
    private bool isCounting;
    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0.0f;
        timerField = GetComponent<Text>();
        isCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            timeCounter += Time.deltaTime;
            timerField.text = ((int)timeCounter).ToString("000");
        }
    }

    public void StartCounting()
    {
        isCounting = true;
    }

    public void StopCounting()
    {
        isCounting = false;
    }

    public void ResetCounter()
    {
        timeCounter = 0.0f;
    }
}
