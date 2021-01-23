using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] string AnimationToPlay;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!string.IsNullOrEmpty(AnimationToPlay))
        {
            animator.Play(AnimationToPlay);
        }
    }
}
