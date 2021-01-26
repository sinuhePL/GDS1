using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueController : MonoBehaviour
{
    private AudioSource myAudioSource;
    private VehicleController myVehicle;
    private GameObject myCanvas;

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
        if(SceneManager.GetActiveScene().name != "MainMenu") myVehicle = GameObject.Find("Vehicle").GetComponent<VehicleController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myCanvas = GameObject.Find("Canvas");
    }

    public void QuitToMainMenu()
    {
        myAudioSource.Play();
        Destroy(myCanvas);
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueGame()
    {
        myAudioSource.Play();
        myVehicle.ReSpawn();
        gameObject.SetActive(false);
    }
}
