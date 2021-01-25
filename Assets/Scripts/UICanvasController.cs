using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICanvasController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField] private Text highScoreField;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("highScore")) highScoreField.text = PlayerPrefs.GetInt("highScore").ToString("000000");
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") Destroy(gameObject);
    }
}
