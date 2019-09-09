using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfoHolder : MonoBehaviour {

    public SaveFile TheSaveFile = new SaveFile();
    

    public static GameInfoHolder Instance { get; private set; }
    public int previousScene = 0;



    public int loadLevel = 0;
    public int health = 0;
    public int gold = 0;
    public int time = 0;
    public Recipe[] NewRecipes;

    public int Stars = 0;


    // Start is called before the first frame update

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += LevelWasLoaded;
        } else {
            Debug.LogError("Found another LevelSelectManager in the same Scene, make sure only 1 LevelSelectManager exist per scene");
            Destroy(gameObject); // Destroy myself is Instance already has a ref
        }

        if (TheSaver.DoesSaveFileExist() == false) {
            Debug.Log("No Save Have Been Made, Creating A New Game");
            TheSaver.NewSaveFile(ref TheSaveFile);
        } else {
            TheSaveFile = TheSaver.LoadSaveFile();
        }

    }

    public bool save = false;

    private void Update() {
        if(save == true) {
            save = false;
      //      TheSaver.NewSaveFile(ref TheSaveFile);
      //      TheSaveFile.LevelSelectData = new LevelSelectData(LevelSelectManager.Instance.AreaMapStartSection);

            TheSaver.Saver(TheSaveFile);//Save Level Select Info When Coming From LevelSelect. TODO Change To The Place Where The Next Scene Is Placed
        }
    }


    private void OnDestroy() {
        if(Instance == this) {
            SceneManager.sceneLoaded -= LevelWasLoaded;
        }

    }

    private void LevelWasLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.buildIndex == 1) {
            if (TheSaveFile.LevelSelectData == null) {//If This Is The First Time Coming To Level Select This Should Be Null
                Debug.Log("CREATING LEVEL SELECT DATA");
                TheSaveFile.LevelSelectData = new LevelSelectData(LevelSelectManager.Instance.AreaMapStartSection);
            }
            //     SaveInfoLevelSelect.Saver(TheSaveFile);//Save Level Select Info When Coming From LevelSelect. TODO Change To The Place Where The Next Scene Is Placed
        } else if (scene.buildIndex == 2) {

        }

        Debug.Log("LOADED WHILE GOING IN?");
        previousScene = scene.buildIndex;
    }

    public void SetLevelInfo(LevelSelectLevelNode info) {
        loadLevel = info.LoadLevel; 
        health = info.Health;
        gold = info.Gold;
        time = info.Time;
        NewRecipes = info.NewRecipe;
        Stars = info.Stars;
    }

}
