using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Dependencies:")]
    [SerializeField] private Transform wheel1;
    [SerializeField] private Transform wheel2;
    [SerializeField] private Transform wheel3;
    [Header("For designers:")]
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

    private float upwardSpeed = 0.0f;
    private bool isJumping = false;
    private float maxVehicleSpeed;
    private float minVehicleSpeed;
    private float currentVehicleSpeed;
    private float sidewaysVehicleSpeed;

    private void moveVehicle()
    {
        if (transform.position.y > 0.0f) upwardSpeed -= moonAccelerationConstant * Time.deltaTime;
        transform.Translate(new Vector3(0.0f, Time.deltaTime * upwardSpeed, 0.0f));
        wheel1.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * currentVehicleSpeed * -110.0f));
        wheel2.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * currentVehicleSpeed * -110.0f));
        wheel3.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * currentVehicleSpeed * -150.0f));
        if (transform.position.y <= 0.0f)
        {
            isJumping = false;
            upwardSpeed = 0.0f;
        }
    }

    private void manageInput()
    {
        if (Input.GetAxis("Fire1") > 0.0f && !isJumping)
        {
            isJumping = true;
            upwardSpeed = jumpSpeed;
        }
        if (Input.GetAxis("Horizontal") > 0.5f && currentVehicleSpeed <= maxVehicleSpeed)
        {
            currentVehicleSpeed += Time.deltaTime * vehicleAcceleration;
            transform.Translate(new Vector3(sidewaysVehicleSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        else if (Input.GetAxis("Horizontal") < -0.5f && currentVehicleSpeed >= minVehicleSpeed)
        {
            currentVehicleSpeed -= Time.deltaTime * vehicleAcceleration;
            transform.Translate(new Vector3(-sidewaysVehicleSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        else if (Input.GetAxis("Horizontal") < 0.5f && currentVehicleSpeed > defaultVehicleSpeed * 1.001f)
        {
            currentVehicleSpeed -= Time.deltaTime * vehicleAcceleration;
            transform.Translate(new Vector3(-sidewaysVehicleSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        else if (Input.GetAxis("Horizontal") > -0.5f && currentVehicleSpeed < defaultVehicleSpeed * 0.999f)
        {
            currentVehicleSpeed += Time.deltaTime * vehicleAcceleration;
            transform.Translate(new Vector3(sidewaysVehicleSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
    }

    public float GetDefaultVehicleSpeed()
    {
        return defaultVehicleSpeed;
    }

    public float GetVehicleAcceleration()
    {
        return vehicleAcceleration;
    }

    public int GetVehicleMaxSpeedChange()
    {
        return maxSpeedChange;
    }

    // Sideways speed is determined based on assumption that time of speed change must be equal to time of sideways move.
    // So sideways move range divided by sideways move speed must be equal to vehicle speed max change divided by vehicle acceleration
    void Start()
    {
        currentVehicleSpeed = defaultVehicleSpeed;
        maxVehicleSpeed = defaultVehicleSpeed * (1 + ((float)maxSpeedChange / 100));
        minVehicleSpeed = defaultVehicleSpeed * (1 - ((float)maxSpeedChange / 100));
        sidewaysVehicleSpeed = 100 * sidewaysRange * vehicleAcceleration / (defaultVehicleSpeed * (100 - maxSpeedChange));
    }

    void Update()
    {
        moveVehicle();
        manageInput();
    }
}
