using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuHoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject blockerPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        blockerPanel.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        blockerPanel.SetActive(true);
    }
}
