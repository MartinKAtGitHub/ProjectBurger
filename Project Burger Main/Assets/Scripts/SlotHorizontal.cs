using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHorizontal : MonoBehaviour
{
    public RectTransform RectTransform { get; set; }
    protected virtual void Awake()
    {
        RectTransform = GetComponent<RectTransform>();        
    }
}
