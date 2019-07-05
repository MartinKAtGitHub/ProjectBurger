using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TheGrill : DropArea, IPointerClickHandler {

    public GrillTemperature GrillSettings;
    [HideInInspector]
    public BurgersMeat BurgerIngredience;
    BurgerDrag _TheBurgerInfo;


    //    readonly float StartTemp = 0.125f;
    public readonly float EndHeat = 0.905f;//This Is Going To Become 1, Its Just That I Need To Make a Sprite With Correct Dimentions.

    public bool DropAreaOccupied; // occupied
    float _GrillTopHeat = 60;

    [SerializeField]
    private float _HeatOfBurger = 0;

    public float HeatOfBurger {
        get {
            return _HeatOfBurger;
        }
    }


    #region Flip Variables

    bool _RotateBackAndForth = true;
    bool _FlipBurger = false;

    readonly float _JumpSpeed = 10f;
    readonly float _RotateSpeed = 5f;

    float _RotateTime = 0;
    float _DownForce = 0;
    readonly float _UpForce = 200;//TODO Think About Screen.Height??

    #endregion







    private void Update() {

        if (GrillSettings.HeatOnOff == true) {//Setting The Heat Of The Grill
            if (_GrillTopHeat < GrillSettings.GrillTopTemperatureValue) {
                _GrillTopHeat += GrillSettings.GrillHeatingRate * Time.deltaTime;
                if (_GrillTopHeat > GrillSettings.GrillTopTemperatureValue)
                    _GrillTopHeat = GrillSettings.GrillTopTemperatureValue;
            }
        } else {
            if (_GrillTopHeat > 0) {
                _GrillTopHeat -= GrillSettings.GrillCoolingRate * Time.deltaTime;
                if (_GrillTopHeat < 0)
                    _GrillTopHeat = 0;
            }
        }

        if (DropAreaOccupied == true) {

            if (_FlipBurger == false) {//Add HeadToBurger

                _HeatOfBurger += (_GrillTopHeat / GrillSettings.GrillTopTemperatureValue) * (1 / BurgerIngredience.MeatCookingTime) * Time.deltaTime;//This Is How Long The Burger Is On, where -1 is negative 100 degrees, and +1 is 100. Normalized Numbers

                if (_TheBurgerInfo.TheBurgerInfos.UpOrDown == true) {
                    AddBurgerHeat(ref _TheBurgerInfo.TheBurgerInfos.MyVariablesUp);
                } else {
                    AddBurgerHeat(ref _TheBurgerInfo.TheBurgerInfos.MyVariablesDown);

                }
            } else {

                _DownForce -= 9.8066f;
                _TheBurgerInfo.TheBurgerInfos.transform.position += ((Vector3.up * _UpForce) + (Vector3.up * _DownForce)) * Time.deltaTime * _JumpSpeed;//This Will Make The Burger Have Abit Rng Jump Hight.

                if (_UpForce + _DownForce < _UpForce / 2) {//WhenToStartFlip Currently At Highest Point

                    if (_RotateBackAndForth == true) {
                        _TheBurgerInfo.transform.rotation = Quaternion.Slerp(_TheBurgerInfo.transform.rotation, Quaternion.Euler(90, 0, 0), (_RotateTime += _RotateSpeed) * Time.deltaTime);

                        if ((_RotateTime * Time.deltaTime) >= 1) {
                            _RotateBackAndForth = false;
                            _RotateTime = 0;
                            _TheBurgerInfo.TheBurgerInfos.UpOrDown = !_TheBurgerInfo.TheBurgerInfos.UpOrDown;
                            SelectingBurgerSide();
                        }

                    } else {
                        _TheBurgerInfo.transform.rotation = Quaternion.Slerp(_TheBurgerInfo.transform.rotation, Quaternion.Euler(0, 0, 0), (_RotateTime += _RotateSpeed) * Time.deltaTime);
                    }
                }

                if (_TheBurgerInfo.transform.position.y < transform.position.y) {//Burger Back On The Grill.
                    _TheBurgerInfo.transform.position = transform.position;
                    _FlipBurger = false;
                    StartCooking();
                }
            }
        }

    }

    public override void OnDrop(PointerEventData eventData) {
        if (DropAreaOccupied == false) {
            _TheBurgerInfo = eventData.pointerDrag.GetComponent<BurgerDrag>();

            if (_TheBurgerInfo != null) {

                if (_TheBurgerInfo.TheIngredientGameObject.ingredient.IngredientType == Ingredient.IngredientTypes.HamBurger_Meat) {

                    Debug.Log("SETTING PARENT");
                    _TheBurgerInfo.ResetPositionParent = transform;
            //        _TheBurgerInfo.transform.SetParent(transform);
                    _TheBurgerInfo.transform.position = transform.position;

                    BurgerIngredience = _TheBurgerInfo.TheIngredientGameObject.ingredient as BurgersMeat;

                    Setup();
                    transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }

    }


    public override void DropAreaOnBeginDrag() {
         
        EvaluateQuality();
        DropAreaOccupied = false;
        transform.GetChild(0).gameObject.SetActive(false);

    }


    public void OnPointerClick(PointerEventData eventData) {
        if (_FlipBurger == false && DropAreaOccupied == true) {
            SetupFlip();
        }
    }



    void Setup() {
        DropAreaOccupied = true;
        SelectingBurgerSide();
        StartCooking();
    }

    void StartCooking() {

        if (_TheBurgerInfo.TheBurgerInfos.UpOrDown == true) {//Setting What BurgerSide To Grill
            _HeatOfBurger = _TheBurgerInfo.TheBurgerInfos.MyVariablesUp._BurgerHeat;
        } else {
            _HeatOfBurger = _TheBurgerInfo.TheBurgerInfos.MyVariablesDown._BurgerHeat;
        }
    }

    void SelectingBurgerSide() {//Setting Burger Info So That The Temperature Can Be Measured Correctly

        if (_TheBurgerInfo.TheBurgerInfos.UpOrDown == true) {//Setting What BurgerSide To Grill
            _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerState[_TheBurgerInfo.TheBurgerInfos.MyVariablesUp._BurgerState].IngredientSprite;
            _TheBurgerInfo.TheIngredientGameObject.ingredient = BurgerIngredience.AllBurgerState[_TheBurgerInfo.TheBurgerInfos.MyVariablesUp._BurgerState];
            //  _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerStages.GetSprite("Hamburgers_Beef_" + _TheBurgerInfo.TheBurgerInfos.MyVariablesUp._BurgerState);
        } else {
            _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerState[_TheBurgerInfo.TheBurgerInfos.MyVariablesDown._BurgerState].IngredientSprite;
            _TheBurgerInfo.TheIngredientGameObject.ingredient = BurgerIngredience.AllBurgerState[_TheBurgerInfo.TheBurgerInfos.MyVariablesDown._BurgerState];
            //  _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerStages.GetSprite("Hamburgers_Beef_" + _TheBurgerInfo.TheBurgerInfos.MyVariablesDown._BurgerState);
        }

    }
    void SetupFlip() {
        _FlipBurger = true;
        _RotateBackAndForth = true;

        _DownForce = 0;
        _RotateTime = 0;
        _GrillTopHeat -= 0.5f;//Reducing Grill Heat Cuz Airflow When Flipping :D
        if (_GrillTopHeat < 0)
            _GrillTopHeat = 0;

    }

    void AddBurgerHeat(ref BurgerQualityVariables burgerside) {//Adding Heat To The Side Of The Burger

        if (burgerside._BurgerHeat < _HeatOfBurger) {//Setting Current Heat
            burgerside._BurgerHeat = _HeatOfBurger;

            if (burgerside._BurgerHeat >= EndHeat) {//Heat

                burgerside._BurgerState = BurgerIngredience.MeatTimers.colorKeys.Length - 1;
                burgerside._BurgerCurrentHeat = burgerside._BurgerHeat;
                    SetupFlip();//MOAHAHAHAH AUTOFLIP
            } else {

                if (burgerside._BurgerCurrentHeat < burgerside._BurgerHeat) {//When The Heat Of The Burger Is Higher Then It Has Ever Been.
                    burgerside._BurgerCurrentHeat = burgerside._BurgerHeat;

                    if (burgerside._BurgerCurrentHeat >= BurgerIngredience.MeatTimers.colorKeys[burgerside._BurgerState].time) {
                        burgerside._BurgerState++;

                        _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerState[burgerside._BurgerState].IngredientSprite;
                        _TheBurgerInfo.TheIngredientGameObject.ingredient = BurgerIngredience.AllBurgerState[burgerside._BurgerState];
                   //     _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerStages.GetSprite("Hamburgers_Beef_" + burgerside._BurgerState);
                        SetupFlip();//MOAHAHAHAH AUTOFLIP
                    }
                }
            }
        }
    }
   

    void EvaluateQuality() {//TODO make this abit more advanced later on. 

        _TheBurgerInfo.TheBurgerInfos.TheQuality = 1 - Mathf.Abs(_TheBurgerInfo.TheBurgerInfos.MyVariablesDown._BurgerHeat - BurgerIngredience.PerfectlyCooked) - Mathf.Abs(_TheBurgerInfo.TheBurgerInfos.MyVariablesUp._BurgerHeat - BurgerIngredience.PerfectlyCooked);
        if (_TheBurgerInfo.TheBurgerInfos.TheQuality < 0)
            _TheBurgerInfo.TheBurgerInfos.TheQuality = 0;

    }




}
