using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndChapterController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject continuePanel;
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
    [SerializeField] private AudioClip endChapterClip;

    private AudioSource myAudioSource;

    private void Awake()
    {
        myAudioSource = gameObject.AddComponent<AudioSource>();
        myAudioSource.clip = endChapterClip;
    }

    private IEnumerator AddBonusPointsAndChangeChapter()
    {
        int additionalPoints = 0, averageTime, numberOfRuns, lastTime;
        myAudioSource.Play();
        averageTime = int.Parse(averageTimeField.text);
        lastTime = int.Parse(timeField.text);
        while (int.Parse(timeField.text) < averageTime)
        {
            timeField.text = (int.Parse(timeField.text) + 1).ToString("000");
            if (!bonusField.enabled) bonusField.enabled = true;
            bonusField.text = (++additionalPoints*100).ToString("000");
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(5.0f);
        if (level.text == "E")
        {
            if (PlayerPrefs.HasKey("numberOfRuns1"))
            {
                numberOfRuns = PlayerPrefs.GetInt("numberOfRuns1");
                averageTime = ((averageTime * numberOfRuns) + lastTime) / ++numberOfRuns;
            }
            else numberOfRuns = 1;
            PlayerPrefs.SetInt("averageTime1", averageTime);
            PlayerPrefs.SetInt("numberOfRuns1", numberOfRuns);
        }
        else if (level.text == "J")
        {
            if (PlayerPrefs.HasKey("numberOfRuns2"))
            {
                numberOfRuns = PlayerPrefs.GetInt("numberOfRuns2");
                averageTime = ((averageTime * numberOfRuns) + lastTime) / ++numberOfRuns;
            }
            else numberOfRuns = 1;
            PlayerPrefs.SetInt("averageTime2", averageTime);
            PlayerPrefs.SetInt("numberOfRuns2", numberOfRuns);
        }
        else
        {
            if (PlayerPrefs.HasKey("numberOfRuns3"))
            {
                numberOfRuns = PlayerPrefs.GetInt("numberOfRuns3");
                averageTime = ((averageTime * numberOfRuns) + lastTime) / ++numberOfRuns;
            }
            else numberOfRuns = 1;
            PlayerPrefs.SetInt("averageTime3", averageTime);
            PlayerPrefs.SetInt("numberOfRuns3", numberOfRuns);
        }
        if (additionalPoints > 0) scoreField.text = (int.Parse(scoreField.text) + int.Parse(bonusField.text)).ToString("000000");
        continuePanel.SetActive(true);
        if (SceneManager.GetActiveScene().name == "Chapter1") SceneManager.LoadScene("Chapter2");
        else if (SceneManager.GetActiveScene().name == "Chapter2") SceneManager.LoadScene("Chapter3");
        timer.GetComponent<TimerController>().ResetCounter();
        timer.GetComponent<TimerController>().StartCounting();
    }

    public void ShowYourself()
    {
        int playerTime;
        timer.GetComponent<TimerController>().StopCounting();
        playerTime = int.Parse(timer.GetComponent<Text>().text);
        if (level.text == "E")
        {
            if (PlayerPrefs.HasKey("averageTime1")) averageTimeField.text = PlayerPrefs.GetInt("averageTime1").ToString("000");
            if(PlayerPrefs.HasKey("topRecord1")) topRecordField.text = PlayerPrefs.GetInt("topRecord1").ToString("000");
        }
        else if (level.text == "J")
        {
            if (PlayerPrefs.HasKey("averageTime2")) averageTimeField.text = PlayerPrefs.GetInt("averageTime2").ToString("000");
            if (PlayerPrefs.HasKey("topRecord2")) topRecordField.text = PlayerPrefs.GetInt("topRecord2").ToString("000");
        }
        else
        {
            if (PlayerPrefs.HasKey("averageTime3")) averageTimeField.text = PlayerPrefs.GetInt("averageTime3").ToString("000");
            if (PlayerPrefs.HasKey("topRecord3")) topRecordField.text = PlayerPrefs.GetInt("topRecord3").ToString("000");
        }
        gameObject.SetActive(true);
        levelField.text = "TIME TO REACH POINT" + '"' +level.text+ '"';
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
        if (playerTime < int.Parse(topRecordField.text))
        {
            brokenRecordText.enabled = true;
            if (level.text == "E") PlayerPrefs.SetInt("topRecord1", playerTime);
            else if (level.text == "J") PlayerPrefs.SetInt("topRecord2", playerTime);
            else PlayerPrefs.SetInt("topRecord3", playerTime);
        }
        else brokenRecordText.enabled = false;
        StartCoroutine(AddBonusPointsAndChangeChapter());
    }
}
