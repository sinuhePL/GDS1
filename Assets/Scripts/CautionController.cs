using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CautionController : MonoBehaviour
{
    private Image cautionImage;
    private Text cautionText;
    private enum cautionEnum { wave, mines};
    [Header("For designers:")]
    [SerializeField] private cautionEnum cautionType; 

    // Start is called before the first frame update
    void Start()
    {
        if(cautionType == cautionEnum.wave)
        {
            cautionImage = GameObject.Find("Caution1 Image").GetComponent<Image>();
            cautionText = GameObject.Find("Caution1 Label").GetComponent<Text>();
        }
        else
        {
            cautionImage = GameObject.Find("Caution2 Image").GetComponent<Image>();
            cautionText = GameObject.Find("Caution2 Label").GetComponent<Text>();
        }
    }

    private IEnumerator ShowCaution()
    {
        bool isOn = false;
        cautionText.enabled = true;
        for (int i = 0; i<8; i++)
        {
            if (isOn) cautionImage.enabled = false;
            else cautionImage.enabled = true;
            isOn = !isOn;
            yield return new WaitForSeconds(0.5f);
        }
        cautionText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vehicle") StartCoroutine(ShowCaution());
    }
}
