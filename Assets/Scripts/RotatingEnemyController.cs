using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemyController : EnemyController
{
    [Tooltip("Rotating speed")]
    [Range(-1000.0f, -100.0f)]
    [SerializeField]
    private float rotateSpeed = -500.0f;

    protected override IEnumerator MoveToPosition()
    {
        float step;

        while (Vector3.Distance(transform.position, animationStartPosition) > 0.001f)
        {
            step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, animationStartPosition, step);
            transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * rotateSpeed));
            yield return 0;
        }
        myAnimator.enabled = true;
    }

    protected override void Start()
    {
        myAnimator = GetComponent<Animator>();
        StartCoroutine(MoveToPosition());
    }
}
