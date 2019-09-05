using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour {

    public AStar AStarSearch = null;
    public DrivingBurgerCar Player = null;

    public LevelInfoBoard LevelInfo;
    public CameraFollower CameraFollow;
    public BarrierInfoBoard BarrierInfo;
    public MerchantInfoBoard MerchantInfo;
    public GetEasyAccessToNodes NodeList;

    public int PlayerGoldAquired = 0;
    public int PlayerGemsAquired = 0;

    [SerializeField]
    private AreaMapPositions _mapStartPosition;
    public AreaMapPositions AreaMapStartSection { get => _mapStartPosition; }


    public static LevelSelectManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("Found another LevelSelectManager in the same Scene, make sure only 1 LevelSelectManager exist per scene");
            Destroy(gameObject); // Destroy myself is Instance already has a ref
        }

    }
    

}
