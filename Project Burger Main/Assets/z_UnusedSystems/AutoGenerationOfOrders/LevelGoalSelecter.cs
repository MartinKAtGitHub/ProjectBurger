using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoalSelecter : MonoBehaviour {

    public float GoldToGet = 10;
    public float TimeLimitInSecond = 100;

    public float CurrentGold = 10;
    public float CurrentTime = 10;

    public float CurrentLinearPath = 0;
    public float NormalLinearPath = 0;

    public bool RandomOrStayCloseToGraph = false;


    public List<MenuItems> ListOfSoldItems2;
    MenuItems selected;
    Meny meny;


    private void Start() {

        meny = GetComponent<Meny>();
        NormalLinearPath = GoldToGet / TimeLimitInSecond;
   //     CurrentLinearPath = NormalLinearPath;

        closest = 1000;
        for (int i = addlowest; i < addhighest; i++) {

            if (Mathf.Abs(meny.Items[i].averageCost - NormalLinearPath) < closest) {
                CurrentAddingIndex = i;
            }

        }

        int saved = Random.Range(0,2);

        if(saved == 1) {
            CurrentAddingIndex = Mathf.Clamp(CurrentAddingIndex + 1, 0, meny.Items.Length - 1);
            goingup = true;
            randomUp = growingnumber;
            randomDown = 0;
        } else {
            CurrentAddingIndex = Mathf.Clamp(CurrentAddingIndex - 1, 0, meny.Items.Length - 1);
            goingup = false;
            randomUp = 0;
            randomDown = -growingnumber;
        }


        //    LastAddedIndex = Random.Range(0, 1);
        //           LastAddedIndex = Random.Range(0, meny.Items.Length);
        selected = meny.Items[CurrentAddingIndex];
               ListOfSoldItems2.Add(selected);
              CurrentTime += selected.TheTime;
             CurrentGold += selected.Cost;


        while (CurrentTime < TimeLimitInSecond) {
            SelectClosestItem();
           CurrentLinearPath = CurrentGold / CurrentTime;

        }




    }

    int LastAddedIndex = 0;
    int CurrentAddingIndex = 0;
    int rngReach = 2;
    int randomRange = 1;

    int addlowest = 0;
    int addhighest = 0;

    int fluxuation = 2;
    float closest = 0;
    int previousadding = 0;


    public float currentcloseval = 0;

    int randomUp = 0;
    int randomDown = 0;

    bool goingup = true;
    int growingnumber = 3;



    void SelectClosestItem() {//simple version, it will choose recipes and will not realy change that much, small changes, only +-2 rng changes then it will snap back to "0" 

        fluxuation = 2 - (int)(CurrentTime / TimeLimitInSecond / 0.5f);
        if (fluxuation < 0)
            fluxuation = 0;

        /*
         
        i have to group the order into different elements. doing this allows me to make an easy order as   
        chees burger x2 + soda and small fries. estemated 50sec which gives 25+25+10+15 == 75g
        
        this is quite nice, but how do i make it?? how do i assemble it??? how do we do difficulty???

        a difficulty would mean that the time it takes to make the burger is close to the time the customer is willing to wait for it. easy could be total time x2, then x1.5 for medium and 1.25  for hard, and 1.05 for insane??
        what will neutralize difficulty? preperation. if a player always cook total amount of burger meats on the grill, and is prepairing ahead of time, then the order would be out almost as soon as it comes. estemated 50sec, then it could be out in 10 sec. 
        its currently not anything we can go around, so we just need to play and make balanzing numbers around the info we gather. if we can have 3 active customers and the player need to give them all food, then burger meat will always be on the grill... soo this might be a problem that is quite small if we look at the bigger picture.
        

        should it be possible to get asked for more burgers then its possible to cook?, is that the way we can neutralize the constant cooking cheese? and eventualy make the player do it naturaly?
      
         */







        

        if (CurrentAddingIndex + randomDown < 0) {
            addlowest = 0;
        } else {
            addlowest = CurrentAddingIndex + randomDown;
        }

        if (CurrentAddingIndex + randomUp > meny.Items.Length - 1) {
            addhighest = meny.Items.Length;
        } else {
            addhighest = CurrentAddingIndex + randomUp;
        }

        closest = 1000;
        previousadding = CurrentAddingIndex;


        for (int i = addlowest; i < addhighest; i++) {

            if (Mathf.Abs(((CurrentGold + meny.Items[i].Cost) / (CurrentTime + meny.Items[i].TheTime)) - NormalLinearPath) < closest) {
                 closest = Mathf.Abs(((CurrentGold + meny.Items[i].Cost) / (CurrentTime + meny.Items[i].TheTime)) - NormalLinearPath);
                CurrentAddingIndex = i;
            }

          
        }
       

        if (previousadding == CurrentAddingIndex) { //if this happen then i know that the curve straightned out, meaning that its switching from easy burgers to hard burgers or other way around
        //    CurrentAddingIndex = Mathf.Clamp(CurrentAddingIndex + Random.Range(-1, 2), 0, meny.Items.Length - 1);//i dont want the same item all the time when the items are turning around, so im making a small rng.
            goingup = !goingup;
           if (goingup == true) {
                randomDown = 0;
                randomUp = growingnumber;//TODO soften this out to make the next item be instant 4 indexes higher
            } else {
                randomDown = -growingnumber;
                randomUp = 0;
            }

        } else {


  //      CurrentAddingIndex = Mathf.Clamp(CurrentAddingIndex + Random.Range(-1, 2), 0, meny.Items.Length - 1);//this allows the graph to be abig wiggily. so this might be iterated upon.
        }


        ListOfSoldItems2.Add(meny.Items[CurrentAddingIndex]);
        CurrentTime += meny.Items[CurrentAddingIndex].TheTime;
        CurrentGold += meny.Items[CurrentAddingIndex].Cost;











        /*     currentcloseval = Mathf.Abs(NormalLinearPath - ((CurrentGold + meny.Items[0].Cost) / (CurrentTime + meny.Items[0].TheTime)));

             for (int i = 0; i < meny.Items.Length; i++) {
                 if (Mathf.Abs(NormalLinearPath - ((CurrentGold + meny.Items[i].Cost) / (CurrentTime + meny.Items[i].TheTime))) < currentcloseval) {//Checking If The Item Added Makes The Current Gold Get Closer To NormaliLine
                     currentcloseval = Mathf.Abs(NormalLinearPath - ((CurrentGold + meny.Items[i].Cost) / (CurrentTime + meny.Items[i].TheTime)));
                     CurrentAddingIndex = i;
                 }
             }

             int a = Random.Range(0, 3);
             if (a == 0) {
                 CurrentAddingIndex += Random.Range(0, randomRange + 1);
             } else if (a == 1) {
                 CurrentAddingIndex -= Random.Range(0, randomRange + 1);
             } else {

             }
             if (CurrentAddingIndex < 0) {
                 CurrentAddingIndex = 0;
             } else if (CurrentAddingIndex >= meny.Items.Length) {
                 CurrentAddingIndex = meny.Items.Length - 1;
             }

             if (Mathf.Abs(LastAddedIndex - CurrentAddingIndex) < rngReach) {//If Range Of Next Item Is Within Range. Currently If The Next Item Is More Then 2 Places Appart, Make It 2 Instead

                 if(LastAddedIndex == CurrentAddingIndex) {//If Im Going To Make The Same Items Twice, Change It So That It Cannot Make It Again
                     if(Random.Range(0,2) == 0) {
                         CurrentAddingIndex += 1;
                         if (CurrentAddingIndex >= meny.Items.Length) {
                             CurrentAddingIndex = meny.Items.Length - 2;
                         }
                     } else {
                         CurrentAddingIndex -= 1;
                         if(CurrentAddingIndex < 0) {
                             CurrentAddingIndex = 1;
                         }
                     }
                 }

                 ListOfSoldItems2.Add(meny.Items[CurrentAddingIndex]);
                 CurrentTime += meny.Items[CurrentAddingIndex].TheTime;
                 CurrentGold += meny.Items[CurrentAddingIndex].Cost;
                 LastAddedIndex = CurrentAddingIndex;

             } else {

                 CurrentAddingIndex = LastAddedIndex + Mathf.Clamp((CurrentAddingIndex - LastAddedIndex), -rngReach, rngReach);
          //       CurrentAddingIndex = LastAddedIndex + Mathf.Clamp((LastAddedIndex - CurrentAddingIndex), -rngReach, rngReach);

                 if (CurrentAddingIndex < 0) {
                     CurrentAddingIndex = 0;
                 }else if(CurrentAddingIndex >= meny.Items.Length) {
                     CurrentAddingIndex = meny.Items.Length - 1;
                 }

                 ListOfSoldItems2.Add(meny.Items[CurrentAddingIndex]);
                 CurrentTime += meny.Items[CurrentAddingIndex].TheTime;
                 CurrentGold += meny.Items[CurrentAddingIndex].Cost;
                 LastAddedIndex = CurrentAddingIndex;

             }

         }*/




        /*

         the current spawning number will try to get as close to the needed value to get to the normalized value which is gold needed to complete the level devided on total time allowed.

        the value will start and choose a random order
        after that it will get a same order from the same position and will have a range(0,5) from its position and it cannot be the same (so not 2).
        then it will find the next food to order and do the check from there what to order but it can only go maximum 5 positions down or up from its current food position (where the foodlist is ordered by gold per sec)

        this will make the food algoryth go up and down, and when it goes below the needed average then it will stop there for 1 round and redo from its position so +-2 positions again.
        then the loop will continue untill it reaches the needed average and it will stop again for 1 round and continue. (stop is not stopping spawn but holding the spawnspot for a extension of the "graph")

     x   1. order the menu list after gold per sec.
        2. make the logic of taking the next food within maximum +-5 spots 
        3. if the spot exceeds the average number in < or > value then it will stop on that position and choose a food which is +-2 from the food.

         */




    }

    Vector2 test1;
    Vector2 test2;
    float cost1 = 0;
    float time1 = 0;
    float cost2 = 0;
    float time2 = 0;
    private void OnDrawGizmos() {
        if(ListOfSoldItems2.Count > 1) {
            cost1 = 0;
            time1 = 0;

            for(int i = 0; i < ListOfSoldItems2.Count - 1; i++) {

                test1.x = i;
                test1.y = NormalLinearPath;
                test2.x = i + 1;
                test2.y = NormalLinearPath;

                Gizmos.color = Color.white;
                Gizmos.DrawLine(test1, test2);



                test1.x = i;
                test1.y = ListOfSoldItems2[i].averageCost;
                test2.x = i;
                test2.y = ListOfSoldItems2[i].averageCost + 0.1f;

                Gizmos.color = Color.black;
                Gizmos.DrawLine(test1, test2);

                test1.x = i;
                test1.y = ListOfSoldItems2[i].averageCost;
                test2.x = i + 1;
                test2.y = ListOfSoldItems2[i + 1].averageCost;

                Gizmos.color = Color.black;
                Gizmos.DrawLine(test1, test2);

                cost1 += ListOfSoldItems2[i].Cost;
                time1 += ListOfSoldItems2[i].TheTime;
                cost2 = cost1 + ListOfSoldItems2[i + 1].Cost;
                time2 = time1 + ListOfSoldItems2[i + 1].TheTime;

                test1.x = i;
                test1.y = cost1 / time1;
                test2.x = i + 1;
                test2.y = cost2 / time2;

                Gizmos.color = Color.green;
                Gizmos.DrawLine(test1, test2);






            }


        }
    }

}
