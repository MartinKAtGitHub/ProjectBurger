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
        CurrentLinearPath = CurrentGold / CurrentTime;
        NormalLinearPath = GoldToGet / TimeLimitInSecond;

    //    LastAddedIndex = Random.Range(0, 1);
        LastAddedIndex = Random.Range(0, meny.Items.Length);
        selected = meny.Items[LastAddedIndex];
        ListOfSoldItems2.Add(selected);
        CurrentTime += selected.TheTime;
        CurrentGold += selected.Cost;
  

        while (CurrentTime < TimeLimitInSecond) {
            CurrentLinearPath = CurrentGold / CurrentTime;
            SelectClosestItem();

        }

    }

    int LastAddedIndex = 0;
    int CurrentAddingIndex = 0;
    int rngReach = 3;

    void SelectClosestItem() {

        float currentcloseval = Mathf.Abs(NormalLinearPath - ((CurrentGold + meny.Items[0].Cost) / (CurrentTime + meny.Items[0].TheTime)));

        for (int i = 0; i < meny.Items.Length; i++) {
            if (Mathf.Abs(NormalLinearPath - ((CurrentGold + meny.Items[i].Cost) / (CurrentTime + meny.Items[i].TheTime))) < currentcloseval) {//Checking If The Item Added Makes The Current Gold Get Closer To NormaliLine
                currentcloseval = Mathf.Abs(NormalLinearPath - ((CurrentGold + meny.Items[i].Cost) / (CurrentTime + meny.Items[i].TheTime)));
                CurrentAddingIndex = i;
            }
        }

        if (Mathf.Abs(LastAddedIndex - CurrentAddingIndex) < rngReach) {

            if(LastAddedIndex == CurrentAddingIndex) {
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
        
    }




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
