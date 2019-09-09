using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEventInfo : MonoBehaviour {

    public int loadLevel = 0;
    public int health = 0;
    public int gold = 0;
    public int time = 0;
    public Recipe[] NewRecipes;

    public int Stars = 0;

    // Start is called before the first frame update

    void Awake() {
        GameObject[] checker = GameObject.FindGameObjectsWithTag("Respawn");
        for(int i = 0; i < checker.Length; i++) {
            if (checker[i] != gameObject && checker[i].GetComponent<LevelEventInfo>() != null)
                Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
   
    public void SetLevelInfo(OnClickWalk info) {
        loadLevel = info.loadLevel;
        health = info.health;
        gold = info.gold;
        time = info.time;
        NewRecipes = info.NewRecipes;
        Stars = info.Stars;

    }

}
