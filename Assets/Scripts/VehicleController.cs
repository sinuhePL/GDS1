﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VehicleController : MonoBehaviour
{
    [Header("Technical:")]
    [SerializeField] private Transform wheel1;
    [SerializeField] private Transform wheel2;
    [SerializeField] private float wheelSpeedMultiplier = 30f;
    [SerializeField] private GameObject bulletUpPrefab;
    [SerializeField] private Transform bulletUpSpawnPoint;
    [SerializeField] private GameObject bulletForwardPrefab;
    [SerializeField] private Transform bulletForwardSpawnPoint;
    [SerializeField] private Transform ground;
    [SerializeField] private Transform backgroundClose;
    [SerializeField] private Transform backgroundMiddle;
    [SerializeField] private Transform backgroundFar;
    [SerializeField] private Transform backgroundStars;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip landClip;
    [SerializeField] private AudioClip shootUpClip;
    [SerializeField] private AudioClip shootForwardClip;
    [SerializeField] private AudioClip engineClip;

    private Text levelField;
    private Text scoreField;
    private GameObject EndPanel;
    private GameObject ContinuePanel;
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
    private float lastLevelPositionMiddleBackground;
    private float lastLevelPositionFarBackground;
    private float lastLevelPositionStarsBackground;
    private Vector3 startingPosition;
    private float moonAccelerationConstant;
    private float closeBackgroundSpeedFactor;
    private float middleBackgroundSpeedFactor;
    private float farBackgroundSpeedFactor;
    private float starsBackgroundSpeedFactor;
    private float jumpSpeed;
    private float vehicleAcceleration;
    private float defaultVehicleSpeed;
    private int maxSpeedChange;
    private int numberOfLives;
    private float sidewaysRange;
    private float upwardPauseSpeed;
    private float currentVehiclePauseSpeed;
    private bool isPaused;
    private Animator animator;
    private AudioSource shootUpAudioSource;
    private AudioSource shootForwardAudioSource;
    private AudioSource jumpAudioSource;
    private AudioSource landAudioSource;
    private AudioSource engineAudioSource;
    private bool levelStarted;
    private Animator dieImage;

    private void moveVehicle()
    {
        if (transform.position.y > 0.2f && !isPaused) upwardSpeed -= moonAccelerationConstant * Time.deltaTime;
        transform.Translate(new Vector3(0.0f, Time.deltaTime * upwardSpeed, 0.0f));
        wheel1.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * currentVehicleSpeed * -wheelSpeedMultiplier));
        wheel2.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * currentVehicleSpeed * wheelSpeedMultiplier));
        if (transform.position.y <= 0.2f)
        {
            if(isJumping) landAudioSource.PlayOneShot(landClip);
            isJumping = false;
            animator.SetBool("IsJumping", false);
            upwardSpeed = 0.0f;
        }
    }

    private void moveGround()
    {
        ground.transform.Translate(new Vector3(-Time.deltaTime * currentVehicleSpeed, 0.0f, 0.0f));
        backgroundClose.transform.Translate(new Vector3(Time.deltaTime * currentVehicleSpeed * closeBackgroundSpeedFactor, 0.0f, 0.0f));
        backgroundMiddle.transform.Translate(new Vector3(Time.deltaTime * currentVehicleSpeed * middleBackgroundSpeedFactor, 0.0f, 0.0f));
        backgroundFar.transform.Translate(new Vector3(Time.deltaTime * currentVehicleSpeed * farBackgroundSpeedFactor, 0.0f, 0.0f));
        backgroundStars.transform.Translate(new Vector3(Time.deltaTime * currentVehicleSpeed * starsBackgroundSpeedFactor, 0.0f, 0.0f));
    }

    private void manageInput()
    {
        if (Input.GetAxis("Fire1") > 0.0f && !isJumping && !isPaused)
        {
            isJumping = true;
            engineAudioSource.pitch = defaultVehicleSpeed/4 * ((float)maxSpeedChange / 100.0f + 1.0f);
            animator.SetBool("IsJumping", true);
            upwardSpeed = jumpSpeed;
            jumpAudioSource.PlayOneShot(jumpClip);
        }
        if (Input.GetAxis("Horizontal") > 0.5f && currentVehicleSpeed <= maxVehicleSpeed && !isPaused)
        {
            currentVehicleSpeed += Time.deltaTime * vehicleAcceleration;
            transform.Translate(new Vector3(sidewaysVehicleSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        else if (Input.GetAxis("Horizontal") < -0.5f && currentVehicleSpeed >= minVehicleSpeed && !isPaused)
        {
            currentVehicleSpeed -= Time.deltaTime * vehicleAcceleration;
            transform.Translate(new Vector3(-sidewaysVehicleSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        else if (Input.GetAxis("Horizontal") < 0.5f && currentVehicleSpeed > defaultVehicleSpeed * 1.001f && !isPaused)
        {
            currentVehicleSpeed -= Time.deltaTime * vehicleAcceleration;
            transform.Translate(new Vector3(-sidewaysVehicleSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        else if (Input.GetAxis("Horizontal") > -0.5f && currentVehicleSpeed < defaultVehicleSpeed * 0.999f && !isPaused)
        {
            currentVehicleSpeed += Time.deltaTime * vehicleAcceleration;
            transform.Translate(new Vector3(sidewaysVehicleSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        if(!isJumping) engineAudioSource.pitch = currentVehicleSpeed/4.0f;
        if (Input.GetAxis("Fire2") > 0.0f && !isFiring && !isPaused)
        {
            isFiring = true;
            Instantiate(bulletUpPrefab, bulletUpSpawnPoint.position, bulletUpPrefab.transform.rotation);
            shootUpAudioSource.PlayOneShot(shootUpClip);
            animator.SetTrigger("Fire_gun_top");
            if (lastForwardBullet == null || !lastForwardBullet.activeInHierarchy)
            {
                shootForwardAudioSource.PlayOneShot(shootForwardClip);
                lastForwardBullet = Instantiate(bulletForwardPrefab, bulletForwardSpawnPoint.position, bulletForwardPrefab.transform.rotation);
                animator.SetTrigger("Fire_gun_front");
            }
        }
        else if(Input.GetAxis("Fire2") == 0.0f && !isPaused) isFiring = false;
        if(Input.GetKeyDown("p"))
        {
            if(isPaused)
            {
                currentVehicleSpeed = currentVehiclePauseSpeed;
                upwardSpeed = upwardPauseSpeed;
            }
            else
            {
                currentVehiclePauseSpeed = currentVehicleSpeed;
                currentVehicleSpeed = 0.0f;
                upwardPauseSpeed = upwardSpeed;
                upwardSpeed = 0.0f;
            }
            isPaused = !isPaused;
            EventsManager.instance.PausePressed();
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

    public void ReSpawn()
    {
        numberOfLives = VehicleOptionsController.instance.GetNumberOfLives();
        VehicleOptionsController.instance.RestoreLife();
        Restore();
    }

    private void Restore()
    {
        engineAudioSource.Play();
        isDestroyed = false;
        VehicleOptionsController.instance.RespawnObstacles();
        currentVehicleSpeed = defaultVehicleSpeed;
        ground.transform.position = new Vector3(lastLevelPositionGround, ground.transform.position.y, ground.transform.position.z);
        backgroundClose.transform.position = new Vector3(lastLevelPositionCloseBackground, backgroundClose.transform.position.y, backgroundClose.transform.position.z);
        backgroundMiddle.transform.position = new Vector3(lastLevelPositionMiddleBackground, backgroundMiddle.transform.position.y, backgroundMiddle.transform.position.z);
        backgroundFar.transform.position = new Vector3(lastLevelPositionFarBackground, backgroundFar.transform.position.y, backgroundFar.transform.position.z);
        backgroundStars.transform.position = new Vector3(lastLevelPositionStarsBackground, backgroundStars.transform.position.y, backgroundStars.transform.position.z);
        transform.position = startingPosition;
    }

    private IEnumerator WaitAndResume()
    {
        yield return new WaitForSeconds(2.5f);
        if (numberOfLives == 0) 
        {
            int hs = PlayerPrefs.GetInt("highScore");
            if (int.Parse(scoreField.text) > hs) PlayerPrefs.SetInt("highScore", int.Parse(scoreField.text));
            ContinuePanel.SetActive(true);
        }
        else Restore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Bomb" || collision.gameObject.tag == "Enemy")
        {
            currentVehicleSpeed = 0.0f;
            isDestroyed = true;
            EventsManager.instance.VehicleDestroyed();
            numberOfLives--;
            VehicleOptionsController.instance.SubsctractLife();
            engineAudioSource.Stop();
            dieImage.Play("Die_fade");
            animator.Play("Rover_explosion");
            StartCoroutine(WaitAndResume());
        }
        else if (collision.gameObject.tag == "Level")
        {
            TextMeshPro myTextMeshPro;
            VehicleOptionsController.instance.ClearObstacleList();
            lastLevelPositionGround = ground.transform.position.x;
            lastLevelPositionCloseBackground = backgroundClose.transform.position.x;
            lastLevelPositionMiddleBackground = backgroundMiddle.transform.position.x;
            lastLevelPositionFarBackground = backgroundFar.transform.position.x;
            lastLevelPositionStarsBackground = backgroundStars.transform.position.x;
            myTextMeshPro = collision.gameObject.GetComponentInChildren<TextMeshPro>();
            levelField.text = myTextMeshPro.text;
            if (levelField.text == "E" || levelField.text == "J" || levelField.text == "O")
            {
                currentVehicleSpeed = 0.0f;
                upwardSpeed = 0.0f;
                isPaused = true;
                EndPanel.GetComponent<EndChapterController>().ShowYourself();
            }
        }
    }

    private void Awake()
    {
        dieImage = GameObject.Find("DieImage").GetComponent<Animator>();
        shootUpAudioSource = gameObject.AddComponent<AudioSource>();
        shootForwardAudioSource = gameObject.AddComponent<AudioSource>();
        jumpAudioSource = gameObject.AddComponent<AudioSource>();
        landAudioSource = gameObject.AddComponent<AudioSource>();
        engineAudioSource = gameObject.AddComponent<AudioSource>();
        engineAudioSource.clip = engineClip;
        engineAudioSource.loop = true;
        levelStarted = false;
    }

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
        if (SceneManager.GetActiveScene().name == "Chapter1") StartCoroutine(Wait5Sec());
        if (SceneManager.GetActiveScene().name == "Chapter2" || SceneManager.GetActiveScene().name == "Chapter3")
        {
            levelStarted = true;
            engineAudioSource.Play();
        }
    }

    private IEnumerator Wait5Sec()
    {
        yield return new WaitForSeconds(1.0f);
        levelStarted = true;
        engineAudioSource.Play();
    }

    void Start()
    {
        levelField = GameObject.Find("PointField").GetComponent<Text>();
        scoreField = GameObject.Find("ScoreCounter").GetComponent<Text>();
        EndPanel = GameObject.Find("EndChapterPanel");
        EndPanel.SetActive(false);
        ContinuePanel = GameObject.Find("ContinuePanel");
        ContinuePanel.SetActive(false);
        moonAccelerationConstant = VehicleOptionsController.instance.GetMoonAccelerationConstant();
        closeBackgroundSpeedFactor = VehicleOptionsController.instance.GetCloseBackgroundSpeedFactor();
        middleBackgroundSpeedFactor = VehicleOptionsController.instance.GetMiddleBackgroundSpeedFactor();
        farBackgroundSpeedFactor = VehicleOptionsController.instance.GetFarBackgroundSpeedFactor();
        starsBackgroundSpeedFactor = VehicleOptionsController.instance.GetStarsBackgroundSpeedFactor();
        jumpSpeed = VehicleOptionsController.instance.GetJumpSpeed();
        vehicleAcceleration = VehicleOptionsController.instance.GetVehicleAcceleration();
        defaultVehicleSpeed = VehicleOptionsController.instance.GetDefaultVehicleSpeed();
        maxSpeedChange = VehicleOptionsController.instance.GetMaxSpeedChange();
        sidewaysRange = VehicleOptionsController.instance.GetSidewaysRange();
        numberOfLives = VehicleOptionsController.instance.GetNumberOfLives();
        currentVehicleSpeed = defaultVehicleSpeed;
        maxVehicleSpeed = defaultVehicleSpeed * (1 + ((float)maxSpeedChange / 100));
        minVehicleSpeed = defaultVehicleSpeed * (1 - ((float)maxSpeedChange / 100));
        // Sideways speed is determined based on assumption that time of speed change must be equal to time of sideways move.
        // So sideways move range divided by sideways move speed must be equal to vehicle speed max change divided by vehicle acceleration
        sidewaysVehicleSpeed = 100 * sidewaysRange * vehicleAcceleration / (defaultVehicleSpeed * (100 - maxSpeedChange));
        isDestroyed = false;
        isJumping = false;
        isFiring = false;
        lastForwardBullet = null;
        lastLevelPositionGround = ground.transform.position.x;
        lastLevelPositionCloseBackground = backgroundClose.transform.position.x;
        lastLevelPositionMiddleBackground = backgroundMiddle.transform.position.x;
        lastLevelPositionFarBackground = backgroundFar.transform.position.x;
        lastLevelPositionStarsBackground = backgroundStars.transform.position.x;
        startingPosition = transform.position;
        isPaused = false;
        animator = GetComponent<Animator>();
}

    void Update()
    {
        if (!isDestroyed && levelStarted)
        {
            moveVehicle();
            moveGround();
            manageInput();
        }
    }
}
