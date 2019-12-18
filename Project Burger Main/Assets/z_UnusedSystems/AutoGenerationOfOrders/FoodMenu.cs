using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMenu : MonoBehaviour {

    public List<FoodGroup> ItemsInOrder = new List<FoodGroup>();
    FoodGroup saved;



    public FoodGroup GetFood() { 
        saved = ItemsInOrder[0];
        ItemsInOrder.RemoveAt(0);

        return saved;
    }



}

[System.Serializable]
public class FoodGroup {
   public List<Recipe> ItemsInOrder = new List<Recipe>();
}
