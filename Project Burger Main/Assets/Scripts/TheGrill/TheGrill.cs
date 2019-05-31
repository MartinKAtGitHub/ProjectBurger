using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TheGrill : DropArea, IPointerClickHandler {



    bool _ChangedBurgerSide = true;
    bool _FlipBurger = false;
 
    BurgersMeat _BurgerIngredience;

    public BurgersMeat TheBurgerIngredience {
        get { return _BurgerIngredience; }
    }

    BurgerInfo _BurgerInfo;
    Image _TheBurgerSprite;

    BurgerQualityVariables _CurrentBurgerSide;


    float _DownForce = 0;
    readonly float _UpForce = 200;//TODO Think About Screen.Height??

    float _RotationValue = 0;
    float _RotateTime = 0;

    readonly float _JumpSpeed = 10f;
    readonly float _RotateSpeed = 5f;
 //   readonly float _RotateSpeed = 2.5f;//


    public bool _StartCooking = false;
    readonly float StartTemp = 0.125f;
    readonly float _EndTime = 0.905f;
    int _KeyCounter = 0;
    public float GrillTemperature = 0;

    bool _RotateBackAndForth = true;
    private void Update() {



        if (_FlipBurger == true) {

            _DownForce -= 9.8066f;
            _BurgerInfo.transform.position += ((Vector3.up * _UpForce) + (Vector3.up * _DownForce)) * Time.deltaTime * _JumpSpeed;//This Will Make The Burger Have Abit Rng Jump Hight.

            

            if (_UpForce + _DownForce < _UpForce / 2) {//WhenToStartFlip Currently At Highest Point

                if (_RotateBackAndForth == true) {
                    _BurgerInfo.transform.rotation = Quaternion.Slerp(_BurgerInfo.transform.rotation, Quaternion.Euler(90, 0, 0), (_RotateTime += _RotateSpeed) * Time.deltaTime);

                    if((_RotateTime * Time.deltaTime) >= 1) {
                        _RotateBackAndForth = false;
                        _RotateTime = 0;
                        _BurgerInfo.UpOrDown = !_BurgerInfo.UpOrDown;
                        SelectingBurgerSide();
                    }

                } else {
                    _BurgerInfo.transform.rotation = Quaternion.Slerp(_BurgerInfo.transform.rotation, Quaternion.Euler(0, 0, 0), (_RotateTime += _RotateSpeed) * Time.deltaTime);
                }
            }

            if (_BurgerInfo.transform.position.y < transform.position.y) {//Burger Back On The Grill.
                _BurgerInfo.transform.position = transform.position;
                _FlipBurger = false;
                StartCooking();
            }

        } else {


            if (_StartCooking == true) {

                if (GrillTemperature < StartTemp) {//In Theory Its Frozen Here Cuz Less Then Start Point.
                    GrillTemperature += (1 / _BurgerIngredience.MeatCookingTime) * Time.deltaTime;
                } else {

                    if (GrillTemperature < _EndTime) {
                        GrillTemperature += (1 / _BurgerIngredience.MeatCookingTime) * Time.deltaTime;

                        if (GrillTemperature >= _EndTime) {
                            _StartCooking = false;
                            _KeyCounter = _BurgerIngredience.MeatTimers.colorKeys.Length - 1;
                        } else {
                            if (GrillTemperature >= _BurgerIngredience.MeatTimers.colorKeys[_KeyCounter].time) {
                                _KeyCounter++;
                            }
                        }
                    }

                }

                if (_KeyCounter > _CurrentBurgerSide._BurgerState) {
                    _TheBurgerSprite.sprite = _BurgerIngredience.AllBurgerStages.GetSprite("Hamburgers_Beef_" + _KeyCounter);
                }

            }
        }

    }

    public override void OnDrop(PointerEventData eventData) {

        if (IsThisDropAreaOccupied == false) {

            if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<IngredientGameObject>() != null) {//The Problem Here Is If I Drag Something And Drop It On Here, I Will Get An Error, So I Need A Check Of Some Sort. eventData.pointerDrag != null, will never happen but just for safety.

                if (eventData.pointerDrag.GetComponent<IngredientGameObject>().ingredient.IngredientType == Ingredient.IngredientTypes.HamBurger_Meat) {
                    _BurgerIngredience = eventData.pointerDrag.GetComponent<IngredientGameObject>().ingredient as BurgersMeat;
                    _BurgerInfo = eventData.pointerDrag.GetComponent<BurgerInfo>();
                    _TheBurgerSprite = eventData.pointerDrag.GetComponent<Image>();

                    base.OnDrop(eventData);
                    IsThisDropAreaOccupied = true;
                    Setup();
                    transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }

    }

    public override void DropAreaOnBeginDrag() {

        SaveInfoToBurgerSide();
        EvaluateQuality();
        _StartCooking = false;
        IsThisDropAreaOccupied = false;
        transform.GetChild(0).gameObject.SetActive(false);

    }

    public void OnPointerClick(PointerEventData eventData) {
        if (_FlipBurger == false && IsThisDropAreaOccupied == true) {
            SaveInfoToBurgerSide();
            SetupFlip();
        }
    }


    void Setup() {
        SelectingBurgerSide();
        StartCooking();
    }


    void SelectingBurgerSide() {//Setting Burger Info So That The Temperature Can Be Measured Correctly

        if (_BurgerInfo.UpOrDown == true) {//Setting What BurgerSide To Grill
            _CurrentBurgerSide = _BurgerInfo.MyVariablesDown;
        } else {
            _CurrentBurgerSide = _BurgerInfo.MyVariablesUp;
        }

        SetPlaceInGradient();

        _TheBurgerSprite.sprite = _BurgerIngredience.AllBurgerStages.GetSprite("Hamburgers_Beef_" + _CurrentBurgerSide._BurgerState);

    }

    void SetPlaceInGradient() {

        for (int i = 0; i < _BurgerIngredience.MeatTimers.colorKeys.Length; i++) {
            if (_CurrentBurgerSide._BurgerHeat < _BurgerIngredience.MeatTimers.colorKeys[i].time) {
                _KeyCounter = i;
                return;
            }
        }

        _KeyCounter = _BurgerIngredience.MeatTimers.colorKeys.Length - 1;

    }

    void SetupFlip() {
        _StartCooking = false;
        _FlipBurger = true;
        _ChangedBurgerSide = false;
        _RotateBackAndForth = true;

        _DownForce = 0;
        _RotateTime = 0;

        if (_RotationValue == 180) {
            _RotationValue = 0;
        } else {
            _RotationValue = 180;
        }

    }

    void StartCooking() {
        _StartCooking = true;
        GrillTemperature = _CurrentBurgerSide._BurgerHeat;

    }

    void SaveInfoToBurgerSide() {
        if (_BurgerInfo.UpOrDown == true) {
            _BurgerInfo.MyVariablesDown._BurgerTime = Time.time;
            _BurgerInfo.MyVariablesDown._BurgerHeat = GrillTemperature;
            _BurgerInfo.MyVariablesDown._BurgerState = _KeyCounter;
        } else {
            _BurgerInfo.MyVariablesUp._BurgerTime = Time.time;
            _BurgerInfo.MyVariablesUp._BurgerHeat = GrillTemperature;
            _BurgerInfo.MyVariablesUp._BurgerState = _KeyCounter;
        }

    }

    void EvaluateQuality() {//TODO make this abit more advanced later on. 

        _BurgerInfo.TheQuality = 1 - Mathf.Abs(_BurgerInfo.MyVariablesDown._BurgerHeat - _BurgerIngredience.PerfectlyCooked) - Mathf.Abs(_BurgerInfo.MyVariablesUp._BurgerHeat - _BurgerIngredience.PerfectlyCooked);
        if (_BurgerInfo.TheQuality < 0)
            _BurgerInfo.TheQuality = 0;

    }




}
