using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meny : MonoBehaviour {

    public MenuItems[] Items;
    public bool UPdateCost = false;

    private void Awake() {
        setAverageCost();
       OrderByCostTime();
    }

    void setAverageCost() {
        for(int i = 0; i < Items.Length; i++) {
            Items[i].averageCost = Items[i].Cost / Items[i].TheTime;
        }
    }

    private void Update() {
        if(UPdateCost == true) {
            UPdateCost = false;
            setAverageCost();
            OrderByCostTime();
        }
    }

    void OrderByCostTime() {
        MenuItems saver;
        float average = 0;//Not Realy Sure Why This Shows As Never Used....
        int position = 0;

        for (int i = 0; i < Items.Length; i++) {
            saver = Items[i];
            average = Items[i].averageCost;
            position = i;

            for (int j = i + 1; j < Items.Length; j++) {


                if (Items[j].averageCost < average) {
                    average = Items[j].averageCost;
                    position = j;
                }
                
            }

            Items[i] = Items[position];
            Items[position] = saver;

        }


    }



}


[System.Serializable]
public class MenuItems {

    public Recipess Items;
    public float Cost;
    public float TheTime;
    public float ChanceOfBeingSPawned = 0;
    public float averageCost;

    [HideInInspector]
    public float min;
    [HideInInspector]
    public float max;


}

public enum Recipess {Coke, Fanta, Sprite, Coffe, SmallFries, NormalFries, BigFries, SixNuggets, NineNuggets, TwelveNuggets};
