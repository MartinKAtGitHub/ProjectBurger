
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ENTER" + name);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("EXIT" + name);

    }
}
