using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private RectTransform creatorsPanel;
    private AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        myAudioSource.Play();
        SceneManager.LoadScene("Chapter1");
    }

    public void EndGame()
    {
        myAudioSource.Play();
        Application.Quit();
    }

    public void ShowCreators()
    {
        myAudioSource.Play();
        creatorsPanel.gameObject.SetActive(true);
    }

    public void HideCreators()
    {
        myAudioSource.Play();
        creatorsPanel.gameObject.SetActive(false);
    }
}
