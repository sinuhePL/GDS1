using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonFocused : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image AnaglyphImage;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        AnaglyphImage.enabled = false;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        AnaglyphImage.enabled = true;

    }
}
