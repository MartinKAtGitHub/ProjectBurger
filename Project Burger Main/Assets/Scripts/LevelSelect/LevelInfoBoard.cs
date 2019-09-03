using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInfoBoard : MonoBehaviour {

    public Text Level;
    public Image[] Stars;
    public Text Gold;
    public Text Time;
    public Image[] Life;
    public Image[] Recipes;

    private LevelSelectLevelNode theLevel;
    
    public void SetLevelInfo(LevelSelectLevelNode info) {
        theLevel = info;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Respawn");

        for(int i = 0; i < objects.Length; i++) {//If There Are More Objects With This Tag.
            if(objects[i].GetComponent<DontDestoyObject>() != null) {
                objects[i].GetComponent<DontDestoyObject>().SetLevelInfo(info);
            }

        }


    }

    private void OnEnable() {

        Level.text = "Level " + theLevel.LoadLevelText;

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

        Gold.text = "" + theLevel.Gold;
        Time.text = "" + theLevel.Time;

        for (int i = 0; i < theLevel.Health; i++) {
            Life[i].gameObject.SetActive(true);
            Life[i].sprite = theLevel.HealthSprite;
        }
        for (int i = theLevel.Health; i < 4; i++) {
            Life[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < theLevel.NewRecipe.Length; i++) {
            //SetActive
            Recipes[i].gameObject.SetActive(true);
            for (int j = 0; j < theLevel.NewRecipe[i].Ingredients.Count; j++) {
                //Build Recipe Image?
            }
        }
        for (int i = theLevel.NewRecipe.Length; i < 5; i++) {
            //SetActive
            Recipes[i].gameObject.SetActive(false);
        }

    }

    public void ExitScoreboard() {
        gameObject.SetActive(false);
        LevelSelectManager.Instance.Player.IgnoreClick = false;
    }

    public void EnterLevel() {
        SceneManager.LoadScene(theLevel.LoadLevel);
    }

}
