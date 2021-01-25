using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    public event Action OnVehicleDestroyed;

    public void VehicleDestroyed()
    {
        OnVehicleDestroyed?.Invoke();
    }

    public event Action OnPausePressed;

    public void PausePressed()
    {
        OnPausePressed?.Invoke();
    }
}
