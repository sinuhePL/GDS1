using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField] private Transform wheel1;
    [SerializeField] private Transform wheel2;
    [SerializeField] private Transform wheel3;
    [SerializeField] private GameObject bulletUpPrefab;
    [SerializeField] private Transform bulletUpSpawnPoint;
    [SerializeField] private GameObject bulletForwardPrefab;
    [SerializeField] private Transform bulletForwardSpawnPoint;
    [SerializeField] private Transform ground;
    [SerializeField] private Transform backgroundClose;
    [SerializeField] private Transform backgroundFar;
    [SerializeField] private Text pointsCounter;
    [Header("For designers:")]
    [Tooltip("Close background speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float closeBackgroundSpeedFactor = 0.2f;
    [Tooltip("Far background speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float farBackgroundSpeedFactor = 0.1f;
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
    [Range(0.5f, 3.0f)]
    [SerializeField] private int numberOfLives = 4;

    private float upwardSpeed = 0.0f;
    private bool isJumping;
    private bool isFiring;
    private float maxVehicleSpeed;
    private float minVehicleSpeed;
    private float currentVehicleSpeed;
    private float sidewaysVehicleSpeed;
    private bool isDestroyed;
    private GameObject lastForwardBullet;
    private float lastLevelPositionGround;
    private float lastLevelPositionCloseBackground;
    private float lastLevelPositionFarBackground;
    private Vector3 startingPosition;

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

    private void moveGround()
    {
        ground.transform.Translate(new Vector3(-Time.deltaTime * currentVehicleSpeed, 0.0f, 0.0f));
        backgroundClose.transform.Translate(new Vector3(Time.deltaTime * currentVehicleSpeed * closeBackgroundSpeedFactor, 0.0f, 0.0f));
        backgroundFar.transform.Translate(new Vector3(Time.deltaTime * currentVehicleSpeed * farBackgroundSpeedFactor, 0.0f, 0.0f));
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
        if (Input.GetAxis("Fire2") > 0.0f && !isFiring)
        {
            isFiring = true;
            Instantiate(bulletUpPrefab, bulletUpSpawnPoint.position, bulletUpPrefab.transform.rotation);
            if(lastForwardBullet == null || !lastForwardBullet.activeInHierarchy) lastForwardBullet = Instantiate(bulletForwardPrefab, bulletForwardSpawnPoint.position, bulletForwardPrefab.transform.rotation);
        }
        else if(Input.GetAxis("Fire2") == 0.0f) isFiring = false;
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

    private IEnumerator WaitAndResume()
    {
        yield return new WaitForSeconds(1.0f);
        isDestroyed = false;
        currentVehicleSpeed = defaultVehicleSpeed;
        ground.transform.position = new Vector3(lastLevelPositionGround, ground.transform.position.y, ground.transform.position.z);
        backgroundClose.transform.position = new Vector3(lastLevelPositionCloseBackground, backgroundClose.transform.position.y, backgroundClose.transform.position.z);
        backgroundFar.transform.position = new Vector3(lastLevelPositionFarBackground, backgroundFar.transform.position.y, backgroundFar.transform.position.z);
        transform.position = startingPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Bomb")
        {
            currentVehicleSpeed = 0.0f;
            isDestroyed = true;
            EventsManager.instance.OnVehicleDestroyed();
            numberOfLives--;
            pointsCounter.text = numberOfLives.ToString();
            StartCoroutine(WaitAndResume());
        }
        else if (collision.gameObject.tag == "Level")
        {
            lastLevelPositionGround = ground.transform.position.x;
            lastLevelPositionCloseBackground = backgroundClose.transform.position.x;
            lastLevelPositionFarBackground = backgroundFar.transform.position.x;
        }
    }

    // Sideways speed is determined based on assumption that time of speed change must be equal to time of sideways move.
    // So sideways move range divided by sideways move speed must be equal to vehicle speed max change divided by vehicle acceleration
    void Start()
    {
        Random.InitState(System.Environment.TickCount);
        currentVehicleSpeed = defaultVehicleSpeed;
        maxVehicleSpeed = defaultVehicleSpeed * (1 + ((float)maxSpeedChange / 100));
        minVehicleSpeed = defaultVehicleSpeed * (1 - ((float)maxSpeedChange / 100));
        sidewaysVehicleSpeed = 100 * sidewaysRange * vehicleAcceleration / (defaultVehicleSpeed * (100 - maxSpeedChange));
        isDestroyed = false;
        isJumping = false;
        isFiring = false;
        lastForwardBullet = null;
        lastLevelPositionGround = ground.transform.position.x;
        lastLevelPositionCloseBackground = backgroundClose.transform.position.x;
        lastLevelPositionFarBackground = backgroundFar.transform.position.x;
        startingPosition = transform.position;
        pointsCounter.text = numberOfLives.ToString();
    }

    void Update()
    {
        if (!isDestroyed)
        {
            moveVehicle();
            moveGround();
            manageInput();
        }
    }
}
