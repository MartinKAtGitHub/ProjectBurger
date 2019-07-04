using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerInfo : MonoBehaviour {

    [HideInInspector]
    public bool UpOrDown = true;//I Need To Know Which Side The Burger Is Flipped

    [HideInInspector]
    public float TheQuality = 1;//Quality Of The GrilledBurger. 1 == 100%

    public BurgerQualityVariables MyVariablesUp = new BurgerQualityVariables(0,0,0,0);
    public BurgerQualityVariables MyVariablesDown = new BurgerQualityVariables(0,0,0,0);


}


public struct BurgerQualityVariables {

    public float _BurgerTime;//This Was Originaly A Time Variable Not A Float, But Time.time == A Float, But This Holds The Time That The Object Did Stuff, Took Off The Grill, Flipped...    public int BurgerStateSideUp = 0;//Burgers Cannot Change State From Cooked To Raw, So Need Something To Save The State It Was/Is In.
    public float _BurgerHeat;//Burgers Resting Will Get Lower Heat Over Time, This And TimeTookOff Should Work Together And Calculate If The Burger Is Cold When Being Served.
    public float _BurgerCurrentHeat;//Burgers Resting Will Get Lower Heat Over Time, This And TimeTookOff Should Work Together And Calculate If The Burger Is Cold When Being Served.

    public int _BurgerState;//Burgers Cannot Change State From Cooked To Raw, So Need Something To Save The State It Was/Is In.

    public BurgerQualityVariables(float a, float b, int c, float d) {
        _BurgerTime = a;
        _BurgerHeat = b;
        _BurgerCurrentHeat = d;
        _BurgerState = c;
    }
}