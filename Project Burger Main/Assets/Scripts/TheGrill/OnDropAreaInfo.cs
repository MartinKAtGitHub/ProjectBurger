﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class OnDropAreaInfo : MonoBehaviour, IDropHandler {

    public abstract void DropAreaOnBeginDrag();//Kinda Still Need This To Know When Something Is Dragged Off The DropArea 
    public abstract void ReadustIfDropFail();//This Was Made For BurgerStack 

    public virtual void OnDrop(PointerEventData eventData) {

    }
}
