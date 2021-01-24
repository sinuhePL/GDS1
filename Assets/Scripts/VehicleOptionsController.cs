using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleOptionsController : MonoBehaviour
{
    [Header("For designers:")]
    [Tooltip("Close background speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float closeBackgroundSpeedFactor = -0.2f;
    [Tooltip("Middle background speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float middleBackgroundSpeedFactor = -0.2f;
    [Tooltip("Far background speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float farBackgroundSpeedFactor = -0.1f;
    [Tooltip("Stars background speed relative to vehicle speed")]
    [Range(-1.0f, 0.0f)]
    [SerializeField] private float starsBackgroundSpeedFactor = -0.1f;
    [Tooltip("Initial upward speed when pressing [7]")]
    [Range(1.0f, 10.0f)]
    [SerializeField] private float jumpSpeed = 1.5f;
    [Tooltip("Moon gravity constant.")]
    [Range(1.0f, 5.0f)]
    [SerializeField] private float moonAccelerationConstant = 3.0f;
    [Tooltip("Default speed of vehicle when no buttons are pressed.")]
    [Range(1.0f, 10.0f)]
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
    [Tooltip("Bomb and enemy drop zone")]
    public Transform[] dropZones;
    [Header("Technical:")]
    [SerializeField] private Image batteryPrefab;
    [SerializeField] private GameObject batteriesGroup;

    public static VehicleOptionsController instance;

    private List<Image> batteriesList;

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
        Image battery;

        Random.InitState(System.Environment.TickCount);
        DontDestroyOnLoad(gameObject);
        batteriesList = new List<Image>();
        for (int i = 0; i < numberOfLives; i++)
        {
            battery = Instantiate(batteryPrefab, batteryPrefab.transform.position, Quaternion.identity).GetComponent<Image>();
            battery.transform.SetParent(batteriesGroup.transform);
            batteriesList.Add(battery);
        }
    }

    public float GetCloseBackgroundSpeedFactor()
    {
        return closeBackgroundSpeedFactor;
    }

    public float GetMiddleBackgroundSpeedFactor()
    {
        return middleBackgroundSpeedFactor;
    }

    public float GetFarBackgroundSpeedFactor()
    {
        return farBackgroundSpeedFactor;
    }

    public float GetStarsBackgroundSpeedFactor()
    {
        return starsBackgroundSpeedFactor;
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

    public void SubsctractLife()
    {
        for (int i = numberOfLives - 1; i >= 0; i--)
        {
            if (batteriesList[i].gameObject.activeSelf)
            {
                batteriesList[i].gameObject.SetActive(false);
                break;
            }
        }
    }
}
