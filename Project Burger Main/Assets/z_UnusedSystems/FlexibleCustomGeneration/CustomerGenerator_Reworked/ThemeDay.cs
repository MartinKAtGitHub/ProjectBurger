using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Idea Here Is To Group Up Customers, So That If We Have A Harry Potter Theme Day, Alot Of Nerds Should Spawn, Or A Sport Day Where The Majority Are Men.
//So That Later If We Have Made 20 Themes Then We Can Simply Drag On Several Themes If We Want A Burthday Party And A Ninja Turtles Theme Day.

//If We Make Many Kinds Of Costumes Like Pirates And Cowboys And Wizards Then We Can Combine Them With No Work And Make A Halloween Theme With No Aditional Work.
//But This Is Still In Its Infancy, Need Several Iterations To Be Where It Should But Currently I Like The Idea.


[CreateAssetMenu(menuName = "ThemeDay", fileName = "Theme")]

//For This To Work Properly There Needs To Be A Menu Place Where Themeday Recipes Are Connected To And Applied.
//Then When A Customer Is Going To Be Spawned Then He Knows Which Theme He Is From And Will Most Likely Get Something From The ThemeDay He Is From. Should Sports Guy Jake Be Able To Buy Timmys Cake?
public class ThemeDay : ScriptableObject {

    public ThemedCustomerGroup[] CustomerSpawnValues;

  //  [HideInInspector]
    public float MinRng = 0;
 //   [HideInInspector]
    public float MaxRng = 0;
 //   [HideInInspector]
    public float ThemeRngChance = 0;//Theme Chance Is Currently Dependent On How High The Spawn Is Off The Different Customer Groups. If You Want A Birthday Party Then The Number Of Guests Sets The Rng Value Of The ThemeDay f.eks: Birthday Party, Sports Day (Expected To Come?)


   private void OnEnable() {//Is Called At The Start Of Every Scene That It Is Used + Every Save -_-
     //   UpdateCustomerGroupRng();
    }
 
    /// <summary>
    /// When Changing Spawn Values On Customers, This Needs To Be Called To Update The Spawn Rates
    /// </summary>
    public void UpdateCustomerGroupRng() {//When The Value Of The Customers Are Change This Need To run

        CustomerSpawnValues[0].MinRng = 0;
        CustomerSpawnValues[0].MaxRng = CustomerSpawnValues[0].SpawnValue;

        for (int i  = 1; i < CustomerSpawnValues.Length; i++) {
            CustomerSpawnValues[i].MinRng = CustomerSpawnValues[i - 1].MaxRng;
            CustomerSpawnValues[i].MaxRng = CustomerSpawnValues[i].MinRng + CustomerSpawnValues[i].SpawnValue;

        }

        ThemeRngChance = CustomerSpawnValues[CustomerSpawnValues.Length - 1].MaxRng;

    }

}




[System.Serializable]
public class ThemedCustomerGroup {

    public CustomerRefrences CustomerToIncreaseSpawnValue;//The Prefab That Holds The Size Of The Object Spawned. (Like Adult Male, Or Child Boy And So On.)
    public float SpawnValue = 1;

    [HideInInspector]
    public float MinRng = 0;
    [HideInInspector]
    public float MaxRng = 0;


}
