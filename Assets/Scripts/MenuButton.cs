using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image anaglyphImage;
    [SerializeField] AudioClip mouseOverClip;
    [SerializeField] AudioClip mouseDownClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        anaglyphImage.enabled = false;
        audioSource.Play();
    }
    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        anaglyphImage.enabled = true;
        audioSource.Stop();
    }
}
