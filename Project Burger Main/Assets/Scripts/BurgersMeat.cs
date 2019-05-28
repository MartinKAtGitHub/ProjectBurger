using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Time the burgers. Flip them and store them
public class BurgersMeat : Draggable {

    [Space(10)]
    public float CookingTime = 1;
 //   public float Temperature = 60;

    public Sprite RawMear;
    public Sprite Complete;
    public Sprite Burnt;


    [HideInInspector]
    public Time timeTookOff;
    [HideInInspector]
    public float _BurgerHeat;
  
   

}





