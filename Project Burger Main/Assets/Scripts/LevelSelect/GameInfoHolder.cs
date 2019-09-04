using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfoHolder : MonoBehaviour {

    public int loadLevel = 0;
    public int health = 0;
    public int gold = 0;
    public int time = 0;
    public Recipe[] NewRecipes;

    public int Stars = 0;


    private Node PlayerPreviousPosition;
    private Node PlayerCurrentPosition;
    private AreaPoints Positions = new AreaPoints();

    // Start is called before the first frame update
    public static GameInfoHolder Instance { get; private set; }
    int previousScene = 0;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += LevelWasLoaded;

        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Found another LevelSelectManager in the same Scene, make sure only 1 LevelSelectManager exist per scene");
            Destroy(gameObject); // Destroy myself is Instance already has a ref
        }

    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= LevelWasLoaded;
    }

    private void LevelWasLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.buildIndex == 2) {
            if (previousScene == 0) {//Load From Previous Position If True
                //TODO LOAD If Player Not At Start Position.
                if (!true) {
                    //If True, Then There Exist A Savefile, Meaning That The Player Are Not At The Start
                } else {//New Fresh Game.
                    LevelSelectManager.Instance.CameraFollow.SetStartValues();
                    Debug.Log("HERE");
                }

            } else {
                LevelSelectManager.Instance.Player.SetPlayerStartPosition(PlayerPreviousPosition, PlayerCurrentPosition);
                LevelSelectManager.Instance.CameraFollow.UpdateAndApplyAreaOffsetValues(Positions);
            }


            Debug.Log("LOADED WHILE GOING IN?");
        }


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


    public void SetPlayerNodes(Node previous, Node current) {
        PlayerPreviousPosition = previous;
        PlayerCurrentPosition = current;
    }


    public void SetCameraPoint(AreaPoints point) {
        Positions = point;
    }

}
