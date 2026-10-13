using UnityEngine;
using UnityEngine.EventSystems;

// TEMPORARY SCRIPT til i can get button navigation from arrow keys
public class ButtonHighlighted : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isHovered = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        Debug.Log("Hovering");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        Debug.Log("Stopped Hovering");
    }
}
