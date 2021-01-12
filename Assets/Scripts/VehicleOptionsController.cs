﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleOptionsController : MonoBehaviour
{
    [Header("For designers:")]
    [Tooltip("Close background speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float closeBackgroundSpeedFactor = -0.8f;
    [Tooltip("Far background speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float farBackgroundSpeedFactor = -0.9f;
    [Tooltip("Initial upward speed when pressing [7]")]
    [Range(1.0f, 5.0f)]
    [SerializeField] private float jumpSpeed = 1.5f;
    [Tooltip("Moon gravity constant.")]
    [Range(1.0f, 5.0f)]
    [SerializeField] private float moonAccelerationConstant = 3.0f;
    [Tooltip("Default speed of vehicle when no buttons are pressed.")]
    [Range(1.0f, 5.0f)]
    [SerializeField] private float defaultVehicleSpeed = 1.5f;
    [Tooltip("Vehicle max speed change in %.")]
    [Range(10, 100)]
    [SerializeField] private int maxSpeedChange = 50;
    [Tooltip("Vehicle acceleration/deceleration")]
    [Range(0.1f, 0.8f)]
    [SerializeField] private float vehicleAcceleration = 0.4f;
    [Tooltip("Vehicle sideways movement range.")]
    [Range(0.5f, 3.0f)]
    [SerializeField] private float sidewaysRange = 1.5f;
    [Tooltip("Number of Lives.")]
    [Range(1, 5)]
    [SerializeField] private static int numberOfLives = 4;

    public static VehicleOptionsController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.Environment.TickCount);
        DontDestroyOnLoad(gameObject);
    }

    public float GetCloseBackgroundSpeedFactor()
    {
        return closeBackgroundSpeedFactor;
    }

    public float GetFarBackgroundSpeedFactor()
    {
        return farBackgroundSpeedFactor;
    }

    public float GetJumpSpeed()
    {
        return jumpSpeed;
    }

    public float GetMoonAccelerationConstant()
    {
        return moonAccelerationConstant;
    }

    public float GetDefaultVehicleSpeed()
    {
        return defaultVehicleSpeed;
    }

    public int GetMaxSpeedChange()
    {
        return maxSpeedChange;
    }

    public float GetVehicleAcceleration()
    {
        return vehicleAcceleration;
    }

    public float GetSidewaysRange()
    {
        return sidewaysRange;
    }

    public int GetNumberOfLives()
    {
        return numberOfLives;
    }
}