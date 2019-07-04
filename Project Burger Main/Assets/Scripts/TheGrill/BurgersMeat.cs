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

    [HideInInspector] //Currently In Construction.
    public float MeatHeatForPerfection = 60f;//This Is The Heat That The Spesific Meat Needs To Be Perfectly Made. (TODO In Future, To Hot Might Burn The Meat) 

    public BurgersMeat[] AllBurgerState;

    public SpriteAtlas AllBurgerStages;
      
}





