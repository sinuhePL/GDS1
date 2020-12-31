using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField] private GameObject vehicle;
    [Header("For designers:")]
    [Tooltip("Ground speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float moveSpeedFactor = -1.0f;


    private float currentVehicleSpeed;
    private float maxVehicleSpeed;
    private float minVehicleSpeed;
    private float vehicleAcceleration;
    private float defaultVehicleSpeed;
    private int maxSpeedChange;
    private bool isVehicleDestroyed;

    private void moveGround()
    {
        transform.Translate(new Vector3(Time.deltaTime * currentVehicleSpeed * moveSpeedFactor, 0.0f, 0.0f));
    }

    private void manageInput()
    {
        if (Input.GetAxis("Horizontal") > 0.5f && currentVehicleSpeed <= maxVehicleSpeed)
        {
            currentVehicleSpeed += Time.deltaTime * vehicleAcceleration;
        }
        else if (Input.GetAxis("Horizontal") < -0.5f && currentVehicleSpeed >= minVehicleSpeed)
        {
            currentVehicleSpeed -= Time.deltaTime * vehicleAcceleration;
        }
        else if (Input.GetAxis("Horizontal") < 0.5f && currentVehicleSpeed > defaultVehicleSpeed)
        {
            currentVehicleSpeed -= Time.deltaTime * vehicleAcceleration;
        }
        else if (Input.GetAxis("Horizontal") > -0.5f && currentVehicleSpeed < defaultVehicleSpeed)
        {
            currentVehicleSpeed += Time.deltaTime * vehicleAcceleration;
        }
    }

    private void StopGroundMove()
    {
        currentVehicleSpeed = 0.0f;
        isVehicleDestroyed = true;
    }

    private void OnDestroy()
    {
        EventsManager.instance.VehicleDestroyed -= StopGroundMove;
    }

    // Start is called before the first frame update
    void Start()
    {
        VehicleController vc;
        vc = vehicle.GetComponent<VehicleController>();
        defaultVehicleSpeed = vc.GetDefaultVehicleSpeed();
        vehicleAcceleration = vc.GetVehicleAcceleration();
        maxSpeedChange = vc.GetVehicleMaxSpeedChange();
        currentVehicleSpeed = defaultVehicleSpeed;
        maxVehicleSpeed = defaultVehicleSpeed * (1 + ((float)maxSpeedChange / 100));
        minVehicleSpeed = defaultVehicleSpeed * (1 - ((float)maxSpeedChange / 100));
        EventsManager.instance.VehicleDestroyed += StopGroundMove;
        isVehicleDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVehicleDestroyed)
        {
            moveGround();
            manageInput();
        }
    }
}
