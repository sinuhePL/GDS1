using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CautionController : MonoBehaviour
{
    private Animator cautionAnimator;
    private enum cautionEnum { wave, mines};
    [Header("For designers:")]
    [SerializeField] private cautionEnum cautionType; 

    // Start is called before the first frame update
    void Start()
    {
        cautionAnimator = GameObject.Find("Warning").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle")
        {
            if (cautionType == cautionEnum.wave) cautionAnimator.Play("UI_Warning_red");
            if (cautionType == cautionEnum.mines) cautionAnimator.Play("UI_Warning_yellow");
        }
    }
}
