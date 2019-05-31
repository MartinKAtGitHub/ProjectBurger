using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

//Time the burgers. Flip them and store them
[CreateAssetMenu(menuName = "BurgersMeat", fileName = "BurgersMeat")]
public class BurgersMeat : Ingredient {

    [Space(10)]

    public Gradient MeatTimers;//This is what tells the user what stage in the cooking prosess the burger is in.
    public float PerfectlyCooked = 0.5f;//The Point On The Gradient That Is Perfect.

    public float MeatCookingTime = 1;//How long does the burger take to cook.

    public SpriteAtlas AllBurgerStages;
      
}





