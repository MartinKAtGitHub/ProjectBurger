using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//get order == set order. test that first

public class ThemeDaySetup : MonoBehaviour {

    public ThemeDay[] ThemeDays;

    float RngMaxRange = 0;
    public float RngMax { get { return RngMaxRange; } }



    void Awake() {
        RemakeEverything();
    }

    public void RemakeEverything() {
        SetThemeGroupRng();
        SetThemeDayRng();
    }

    void SetThemeGroupRng() {
        for (int i = 0; i < ThemeDays.Length; i++) {
            ThemeDays[i].UpdateCustomerGroupRng();
        }
    }

    void SetThemeDayRng() {
        ThemeDays[0].MinRng = 0;//Setting Theme Rng Based On Amount Of Ppl Expected From ThemeDay
        ThemeDays[0].MaxRng = ThemeDays[0].ThemeRngChance;

        for (int i = 1; i < ThemeDays.Length; i++) {
            ThemeDays[i].MinRng = ThemeDays[i - 1].MaxRng;
            ThemeDays[i].MaxRng = ThemeDays[i].MinRng + ThemeDays[i].ThemeRngChance;
        }

        RngMaxRange = ThemeDays[ThemeDays.Length - 1].MaxRng;
    }




    public CustomerRefrences CusomterSpawnRandom() {//TODO This Can Be Improved By Adding A % Search Based Algorythm, If rng Is 20% Of Max Amount, Then I Start At 20% Spot Of List.Count And Go From There.

         return GetRngTheme().CustomerSpawnValues[Random.Range(0, GetRngTheme().CustomerSpawnValues.Length)].CustomerToIncreaseSpawnValue;//Currently The Theme Logic I Had In Mind Didnt Work As I Wanted, Gave Alot Of Controle, But Needs A Iteration Or Two To Realy Get Going


        /* 
     ThemeDay ThemeDayChecker = null;
    float Checker = 0;
         
       ThemeDayChecker = GetRngTheme();
          Checker = Random.Range(0, ThemeDayChecker.ThemeRngChance);

          for (int i = 0; i < ThemeDayChecker.CustomerSpawnValues.Length; i++) {
              if (Checker >= ThemeDayChecker.CustomerSpawnValues[i].MinRng && Checker <= ThemeDayChecker.CustomerSpawnValues[i].MaxRng) {//If rng Is Between MinMax Then This Shall Be Spawned
               //   Debug.Log("Getting Customer " + ThemeDayChecker.CustomerSpawnValues[i].CustomerToIncreaseSpawnValue.name);
                  return ThemeDayChecker.CustomerSpawnValues[i].CustomerToIncreaseSpawnValue;
              }
          }

          Debug.LogError("Returned 0 Cuz Theme Didnt Excist");
          return ThemeDayChecker.CustomerSpawnValues[0].CustomerToIncreaseSpawnValue;*/

    }

    public ThemeDay GetRngTheme() {
        return ThemeDays[0];//Currently The Theme Logic I Had In Mind Didnt Work As I Wanted, Gave Alot Of Controle, But Needs A Iteration Or Two To Realy Get Going



      /*  Checker = Random.Range(0, RngMaxRange);

        for (int i = 0; i < ThemeDays.Length; i++) {
            if (Checker >= ThemeDays[i].MinRng && Checker <= ThemeDays[i].MaxRng) {//If rng Is Between MinMax Then This Shall Be Spawned
             //   Debug.Log("Getting Theme " + ThemeDays[i].name);
                return ThemeDays[i];
            }
        }

        Debug.LogError("Returned 0 Cuz Theme Didnt Excist");
        return ThemeDays[0];*/
    }


}






