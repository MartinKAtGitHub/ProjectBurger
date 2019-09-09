using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OnClickWalk : MonoBehaviour, IPointerClickHandler {

    public int loadLevel = 0;

    public int LevelText = 1;
    public int health = 3;
    public int gold = 750;
    public int time = 1000;
    public Recipe[] NewRecipes;

    public int Stars = 0;
    public Sprite EarnedStar;
    public Sprite NotEarnedStar;
    public Sprite healthSprite;

    private Node myNode;

    public void OnPointerClick(PointerEventData eventData) {
        LevelSelectManager.Instance.Player.Clicked(myNode);


        if(LevelSelectManager.Instance.LevelInfo.gameObject.activeSelf == true) {
            LevelSelectManager.Instance.LevelInfo.gameObject.SetActive(false);
        }
        LevelSelectManager.Instance.LevelInfo.SetLevelInfo(this);
    }

    private void Awake() {
   //     Stars = PlayerPrefs.GetInt("Level" + Level, 0);
        myNode = GetComponent<Node>();
    }

    public void GoToLevel() {
        SceneManager.LoadScene(loadLevel);
    }

}
