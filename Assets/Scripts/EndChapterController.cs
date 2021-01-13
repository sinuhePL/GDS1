using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndChapterController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField] private GameObject timer;
    [SerializeField] private Text level;
    [SerializeField] private Text levelField;
    [SerializeField] private Text timeField;
    [SerializeField] private Text scoreField;
    [SerializeField] private Text averageTimeField;
    [SerializeField] private Text topRecordField;
    [SerializeField] private Text goodBonusText;
    [SerializeField] private Text noBonusText;
    [SerializeField] private Text bonusField;
    [SerializeField] private Text brokenRecordText;

    private IEnumerator AddBonusPointsAndChangeChapter()
    {
        int additionalPoints = 0;
        while(int.Parse(timeField.text) < int.Parse(averageTimeField.text))
        {
            timeField.text = (int.Parse(timeField.text) + 1).ToString("000");
            if (!bonusField.enabled) bonusField.enabled = true;
            bonusField.text = (++additionalPoints*100).ToString("000");
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(5.0f);
        if(additionalPoints > 0) scoreField.text = (int.Parse(scoreField.text) + int.Parse(bonusField.text)).ToString("000000");
        if (SceneManager.GetActiveScene().name == "Chapter1") SceneManager.LoadScene("Chapter2");
        else if (SceneManager.GetActiveScene().name == "Chapter2") SceneManager.LoadScene("Chapter3");
        timer.GetComponent<TimerController>().ResetCounter();
        timer.GetComponent<TimerController>().StartCounting();
    }

    public void ShowYourself()
    {
        int playerTime;
        playerTime = int.Parse(timer.GetComponent<Text>().text);
        timer.GetComponent<TimerController>().StopCounting();
        gameObject.SetActive(true);
        levelField.text = '"'+level.text+'"';
        timeField.text = timer.GetComponent<Text>().text;
        if (playerTime < int.Parse(averageTimeField.text))
        {
            goodBonusText.enabled = true;
            noBonusText.enabled = false;
        }
        else
        {
            goodBonusText.enabled = false;
            noBonusText.enabled = true;
        }
        if (playerTime < int.Parse(topRecordField.text)) brokenRecordText.enabled = true;
        else brokenRecordText.enabled = false;
        StartCoroutine(AddBonusPointsAndChangeChapter());
    }
}
