using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoParentController : MonoBehaviour
{
    [Header("Technical:")]
    [Tooltip("Position where animation starts")]
    [SerializeField]
    protected Vector3 animationStartPosition = new Vector3(-3.0f, 6.0f, 0.0f);
    [Header("For designers:")]
    [Tooltip("Circle change speed.")]
    [Range(0.0f, 10.0f)]
    [SerializeField] private float circleChangeSpeed = 5.0f;
    [Tooltip("Move to circe position speed.")]
    [Range(1.0f, 20.0f)]
    [SerializeField] private float moveSpeed = 10.0f;
    [Tooltip("Is ufo rotating?")]
    [SerializeField] private bool isRotating;
    [Tooltip("Rotating speed")]
    [Range(-1000.0f, -100.0f)]
    [SerializeField] private float rotateSpeed = -500.0f;
    [Tooltip("Probability of changeing circle height level.")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float circleLevelChangeProbability = 0.5f;
    [Tooltip("Circle level distance.")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float circleLevelDistance = 0.5f;
    [Tooltip("Number of circle levels.")]
    [Range(1,6)]
    [SerializeField] private int maxCircleLevels = 3;

    private Animator myChildAnimator;
    private int circleLevel;

    protected virtual IEnumerator MoveToPosition()
    {
        float step;

        while (Vector3.Distance(transform.position, animationStartPosition) > 0.001f)
        {
            step = moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, animationStartPosition, step);
            if(isRotating) transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * rotateSpeed));
            yield return 0;
        }
        transform.rotation = Quaternion.identity;
        if(myChildAnimator != null) myChildAnimator.enabled = true;
    }

    protected virtual IEnumerator MoveToNewCircle(bool down)
    {
        float step;
        Vector3 newPosition;

        newPosition = transform.position;
        if (down) newPosition.y -= circleLevelDistance;
        else newPosition.y += circleLevelDistance;
        while(Mathf.Abs(transform.position.y - newPosition.y) > 0.001f)
        {
            step = circleChangeSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            yield return 0;
        }
    }

    public void ChangeCircleLevel()
    {
        if (Random.Range(0.0f, 1.0f) < circleLevelChangeProbability)
        {
            if (circleLevel == 1)
            {
                StartCoroutine(MoveToNewCircle(true));
                circleLevel++;
            }
            else if (circleLevel == maxCircleLevels)
            {
                StartCoroutine(MoveToNewCircle(false));
                circleLevel--;
            }
            else
            {
                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    StartCoroutine(MoveToNewCircle(true));
                    circleLevel++;
                }
                else
                {
                    StartCoroutine(MoveToNewCircle(false));
                    circleLevel--;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myChildAnimator = GetComponentInChildren<Animator>();
        StartCoroutine(MoveToPosition());
        circleLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
