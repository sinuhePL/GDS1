using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

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
