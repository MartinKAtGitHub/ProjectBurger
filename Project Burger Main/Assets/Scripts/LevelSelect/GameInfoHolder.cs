using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfoHolder : MonoBehaviour {

    public SaveFileLevelSelect TheSaveFile = null;
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

        TheSaveFile = SaveInfoLevelSelect.LoadSaveFile();

        if (TheSaveFile == null) {
            Debug.Log("No Save Have Been Made, Creating A New Game");
            SaveInfoLevelSelect.NewSaveFile(ref TheSaveFile);
        }

    }

    public bool save = false;

    private void Update() {
        if(save == true) {
            save = false;
            SaveInfoLevelSelect.Saver(TheSaveFile);//Save Level Select Info When Coming From LevelSelect. TODO Change To The Place Where The Next Scene Is Placed
        }
    }


    private void OnDestroy() {
        SceneManager.sceneLoaded -= LevelWasLoaded;
    }

    private void LevelWasLoaded(Scene scene, LoadSceneMode mode) {
        if(previousScene > 1) {
            SaveInfoLevelSelect.Saver(TheSaveFile);//Save Level Select Info When Coming From LevelSelect. TODO Change To The Place Where The Next Scene Is Placed
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
