using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoBoard : MonoBehaviour {

    public Text Level;
    public Image[] Stars;
    public Text Gold;
    public Text Time;
    public Image[] Life;
    public Image[] Recipes;

    private OnClickWalk theLevel;
   [SerializeField] private GoToLevel goToLevel;
    
    public void SetLevelInfo(OnClickWalk info) {
        theLevel = info;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Respawn");

        for(int i = 0; i < objects.Length; i++) {//If There Are More Objects With This Tag.
            if(objects[i].GetComponent<LevelEventInfo>() != null) {
                objects[i].GetComponent<LevelEventInfo>().SetLevelInfo(info);
            }

        }


    }

    private void OnEnable() {

        Level.text = "Level " + theLevel.LevelText;

        if (theLevel.Stars > 0) {
            for (int i = 0; i < theLevel.Stars; i++) {
                Stars[i].sprite = theLevel.EarnedStar;
            }
            for (int i = theLevel.Stars; i < 3; i++) {
                Stars[i].sprite = theLevel.NotEarnedStar;
            }
        } else {
            for (int i = 0; i < 3; i++) {
                Stars[i].sprite = theLevel.NotEarnedStar;
            }
        }

        Gold.text = "" + theLevel.gold;
        Time.text = "" + theLevel.time;

        for (int i = 0; i < theLevel.health; i++) {
            Life[i].gameObject.SetActive(true);
            Life[i].sprite = theLevel.healthSprite;
        }
        for (int i = theLevel.health; i < 4; i++) {
            Life[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < theLevel.NewRecipes.Length; i++) {
            //SetActive
            Recipes[i].gameObject.SetActive(true);
            for (int j = 0; j < theLevel.NewRecipes[i].Ingredients.Count; j++) {
                //Build Recipe Image?
            }
        }
        for (int i = theLevel.NewRecipes.Length; i < 5; i++) {
            //SetActive
            Recipes[i].gameObject.SetActive(false);
        }

        goToLevel.levelID = theLevel.loadLevel;

    }
}
