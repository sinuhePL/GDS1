using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive: MonoBehaviour
{
    [Header("Dependencies:")]
    [SerializeField] private Transform wheel1;
    [SerializeField] private Transform wheel2;
    [SerializeField] private Transform wheel3;
    [SerializeField] private Transform background1;
    [SerializeField] private Transform background2;
    [SerializeField] private Transform UpSpawnPoint;
    [SerializeField] private Transform ForwardSpawnPoint;
    [Header("For designers:")]
    [Range(1.0f, 5.0f)]
    [SerializeField] private float vehicleDefaultSpeed = 1.5f;
    [Range(1.0f, 5.0f)]
    [SerializeField] private float jumpSpeed = 1.5f;
    [Range(1.0f, 5.0f)]
    [SerializeField] private float moonAccelerationConstant = 3.0f;

    private bool isJumping = false;
    private Vector3 currentSpeed;
    private Camera myCamera;

    private void moveVehicle()
    {
        if(transform.position.y > 0.0f) currentSpeed.y -= moonAccelerationConstant * Time.deltaTime;
        transform.Translate(Time.deltaTime * currentSpeed);
        background1.Translate(new Vector3(Time.deltaTime * currentSpeed.x * -0.3f, 0.0f, 0.0f));
        background2.Translate(new Vector3(Time.deltaTime * currentSpeed.x * -0.1f, 0.0f, 0.0f));
        wheel1.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * currentSpeed.x * -100.0f));
        wheel2.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * currentSpeed.x * -100.0f));
        wheel3.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * currentSpeed.x * -150.0f));
        myCamera.transform.Translate(0.0f, -Time.deltaTime * currentSpeed.y, 0.0f);
        background1.transform.Translate(0.0f, -Time.deltaTime * currentSpeed.y, 0.0f);
        background2.transform.Translate(0.0f, -Time.deltaTime * currentSpeed.y, 0.0f);
    }

    private void manageInput()
    {
        if(Input.GetAxis("Fire1") > 0.0f && !isJumping)
        {
            isJumping = true;
            currentSpeed.y = jumpSpeed;
        }
    }

    private void Start()
    {
        currentSpeed = new Vector3(vehicleDefaultSpeed, 0.0f, 0.0f);
        myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        manageInput();
        moveVehicle();
        if(transform.position.y <= 0.0f)
        {
            isJumping = false;
            currentSpeed.y = 0.0f;
        }
    }
}
