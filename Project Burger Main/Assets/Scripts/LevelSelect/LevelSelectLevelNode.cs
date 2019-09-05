using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectLevelNode : NodeBehaviour, IPointerClickHandler {

    [SerializeField]
    private int _loadLevel = 0;

    [SerializeField]
    private int _levelText = 1;
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private int _gold = 750;
    [SerializeField]
    private int _goldEarned = 0;

    [SerializeField]
    private int _time = 1000;
    [SerializeField]
    private Recipe[] _newRecipes = null;

    [SerializeField]
    private int _stars = 0;
    [SerializeField]
    private int _timesPlayer = 0;

    [SerializeField]
    private Sprite _earnedStar = null;
    [SerializeField]
    private Sprite _notEarnedStar = null;
    [SerializeField]
    private Sprite _healthSprite = null;


    private Node _myNode;

    public int LoadLevel { get { return _loadLevel; } }
    public int LoadLevelText { get { return _levelText; } }
    public int TimesPlayer { get { return _timesPlayer; } }
    public int Health { get { return _health; } }
    public int Gold { get { return _gold; } }
    public int GoldEarned { get { return _goldEarned; } }
    public int Time { get { return _time; } }
    public Recipe[] NewRecipe { get { return _newRecipes; } }
    public int Stars { get { return _stars; } } 
    public Sprite EarnedStar { get { return _earnedStar; } }
    public Sprite NotEarnedStar { get { return _notEarnedStar; } }
    public Sprite HealthSprite { get { return _healthSprite; } }  



     

    public void OnPointerClick(PointerEventData eventData) {
        LevelSelectManager.Instance.Player.Clicked(_myNode);

    }

    private void Start() {
        _myNode = GetComponent<Node>();
        SetInfo(GameInfoHolder.Instance.TheSaveFile.LevelInfo.Levels[_levelText]);//This Sometimes Makes A NULLREF From Levels[_levelText]  The Place im Setting This Is In Awake, While This Code Is in Start. Sooooooooooo im ont sure whats wrong here, need to do some checkes

    }

    void SetInfo(TheLevels info) {
        _goldEarned = info.GoldEarned;
        _stars = info.StarsGained;
        _timesPlayer = info.TimesPlayer;

    }


     

    public override void SteppingOffEndNodeBehaviour() {
        LevelSelectManager.Instance.LevelInfo.gameObject.SetActive(false);

    }


    public override void TransitionNodeBehaviour() {
        LevelSelectManager.Instance.Player.StopOnNode();

    }


    public override void SteppingOnEndNodeBehaviour() {
        LevelSelectManager.Instance.Player.StopOnNode();
        LevelSelectManager.Instance.LevelInfo.SetLevelInfo(this);
        LevelSelectManager.Instance.LevelInfo.gameObject.SetActive(true);

    }

}
