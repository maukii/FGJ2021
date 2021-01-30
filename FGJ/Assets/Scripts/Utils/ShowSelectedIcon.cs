using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowSelectedIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject selection;
    public void OnPointerEnter(PointerEventData eventData)
    {
        selection.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        selection.SetActive(false);
    }
}
