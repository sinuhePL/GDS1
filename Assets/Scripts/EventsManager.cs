using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    public event Action VehicleDestroyed;

    public void OnVehicleDestroyed()
    {
        VehicleDestroyed?.Invoke();
    }
}
