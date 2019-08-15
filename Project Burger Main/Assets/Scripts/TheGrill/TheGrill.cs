using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TheGrill : MonoBehaviour, IPointerClickHandler, IDropHandler
{

    public GrillTemperature GrillSettings;
    [HideInInspector]
    public BurgersMeat BurgerIngredience;
    BurgerDrag _theBurgerDrag;


    //    readonly float StartTemp = 0.125f;
    public readonly float EndHeat = 0.905f;//This Is Going To Become 1, Its Just That I Need To Make a Sprite With Correct Dimentions.

    public bool DropAreaOccupied; // occupied
    float _GrillTopHeat = 60;

    [SerializeField]
    private float _HeatOfBurger = 0;

    public float HeatOfBurger
    {
        get
        {
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







    private void Update()
    {

        if (GrillSettings.HeatOnOff == true)
        {//Setting The Heat Of The Grill
            if (_GrillTopHeat < GrillSettings.GrillTopTemperatureValue)
            {
                _GrillTopHeat += GrillSettings.GrillHeatingRate * Time.deltaTime;
                if (_GrillTopHeat > GrillSettings.GrillTopTemperatureValue)
                    _GrillTopHeat = GrillSettings.GrillTopTemperatureValue;
            }
        }
        else
        {
            if (_GrillTopHeat > 0)
            {
                _GrillTopHeat -= GrillSettings.GrillCoolingRate * Time.deltaTime;
                if (_GrillTopHeat < 0)
                    _GrillTopHeat = 0;
            }
        }

        if (DropAreaOccupied == true)
        {

            if (_FlipBurger == false)
            {//Add HeadToBurger

                _HeatOfBurger += (_GrillTopHeat / GrillSettings.GrillTopTemperatureValue) * (1 / BurgerIngredience.MeatCookingTime) * Time.deltaTime;//This Is How Long The Burger Is On, where -1 is negative 100 degrees, and +1 is 100. Normalized Numbers

                if (_theBurgerDrag.TheBurgerInfos.UpOrDown == true)
                {
                    AddBurgerHeat(ref _theBurgerDrag.TheBurgerInfos.MyVariablesUp);
                }
                else
                {
                    AddBurgerHeat(ref _theBurgerDrag.TheBurgerInfos.MyVariablesDown);

                }
            }
            else
            {

                _DownForce -= 9.8066f;
                _theBurgerDrag.TheBurgerInfos.transform.position += ((Vector3.up * _UpForce) + (Vector3.up * _DownForce)) * Time.deltaTime * _JumpSpeed;//This Will Make The Burger Have Abit Rng Jump Hight.

                if (_UpForce + _DownForce < _UpForce / 2)
                {//WhenToStartFlip Currently At Highest Point

                    if (_RotateBackAndForth == true)
                    {
                        _theBurgerDrag.transform.rotation = Quaternion.Slerp(_theBurgerDrag.transform.rotation, Quaternion.Euler(90, 0, 0), (_RotateTime += _RotateSpeed) * Time.deltaTime);

                        if ((_RotateTime * Time.deltaTime) >= 1)
                        {
                            _RotateBackAndForth = false;
                            _RotateTime = 0;
                            _theBurgerDrag.TheBurgerInfos.UpOrDown = !_theBurgerDrag.TheBurgerInfos.UpOrDown;
                            SelectingBurgerSide();
                        }

                    }
                    else
                    {
                        _theBurgerDrag.transform.rotation = Quaternion.Slerp(_theBurgerDrag.transform.rotation, Quaternion.Euler(0, 0, 0), (_RotateTime += _RotateSpeed) * Time.deltaTime);
                    }
                }

                if (_theBurgerDrag.transform.position.y < transform.position.y)
                {//Burger Back On The Grill.
                    _theBurgerDrag.transform.position = transform.position;
                    _FlipBurger = false;
                    StartCooking();
                }
            }
        }

    }


    public void OnDrop(PointerEventData eventData) 
    {
        if (DropAreaOccupied == false)
        {
            _theBurgerDrag = eventData.pointerDrag.GetComponent<BurgerDrag>();
            if (_theBurgerDrag != null) {
                //   if (_theBurgerDrag.TheIngredientGameObject.ingredient.IngredientType == Ingredient.IngredientTypes.HamBurger_Meat_Raw){//only raw burgers can get on the grill.

                _theBurgerDrag.ResetPositionParent = transform;
                _theBurgerDrag.TheGrill = this;

                BurgerIngredience = _theBurgerDrag.TheIngredientGameObject.ingredient as BurgersMeat;

                Setup();

                //   }
            }
        }
    }


    public void DropAreaOnBeginDrag()
    {
        EvaluateQuality();
        DropAreaOccupied = false;
   //     transform.GetChild(0).gameObject.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_FlipBurger == false && DropAreaOccupied == true)
        {
            SetupFlip();
        }
    }



    public void Setup()
    {
        DropAreaOccupied = true;
        SelectingBurgerSide();
        StartCooking();
   //     transform.GetChild(0).gameObject.SetActive(true);
    }

    void StartCooking()
    {

        if (_theBurgerDrag.TheBurgerInfos.UpOrDown == true)
        {//Setting What BurgerSide To Grill
            _HeatOfBurger = _theBurgerDrag.TheBurgerInfos.MyVariablesUp._BurgerHeat;
        }
        else
        {
            _HeatOfBurger = _theBurgerDrag.TheBurgerInfos.MyVariablesDown._BurgerHeat;
        }
    }

    void SelectingBurgerSide()
    {//Setting Burger Info So That The Temperature Can Be Measured Correctly

        if (_theBurgerDrag.TheBurgerInfos.UpOrDown == true)
        {//Setting What BurgerSide To Grill
            _theBurgerDrag.TheImage.sprite = BurgerIngredience.AllBurgerState[_theBurgerDrag.TheBurgerInfos.MyVariablesUp._BurgerState].IngredientSprite;
            _theBurgerDrag.TheIngredientGameObject.ingredient = BurgerIngredience.AllBurgerState[_theBurgerDrag.TheBurgerInfos.MyVariablesUp._BurgerState];
            //  _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerStages.GetSprite("Hamburgers_Beef_" + _TheBurgerInfo.TheBurgerInfos.MyVariablesUp._BurgerState);
        }
        else
        {
            _theBurgerDrag.TheImage.sprite = BurgerIngredience.AllBurgerState[_theBurgerDrag.TheBurgerInfos.MyVariablesDown._BurgerState].IngredientSprite;
            _theBurgerDrag.TheIngredientGameObject.ingredient = BurgerIngredience.AllBurgerState[_theBurgerDrag.TheBurgerInfos.MyVariablesDown._BurgerState];
            //  _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerStages.GetSprite("Hamburgers_Beef_" + _TheBurgerInfo.TheBurgerInfos.MyVariablesDown._BurgerState);
        }

    }
    void SetupFlip()
    {
        _FlipBurger = true;
        _RotateBackAndForth = true;

        _DownForce = 0;
        _RotateTime = 0;
        _GrillTopHeat -= 0.5f;//Reducing Grill Heat Cuz Airflow When Flipping :D
        if (_GrillTopHeat < 0)
            _GrillTopHeat = 0;

    }

    void AddBurgerHeat(ref BurgerQualityVariables burgerside)
    {//Adding Heat To The Side Of The Burger

        if (burgerside._BurgerHeat < _HeatOfBurger)
        {//Setting Current Heat
            burgerside._BurgerHeat = _HeatOfBurger;

            if (burgerside._BurgerHeat >= EndHeat)
            {//Heat

                burgerside._BurgerState = BurgerIngredience.MeatTimers.colorKeys.Length - 1;
                burgerside._BurgerCurrentHeat = burgerside._BurgerHeat;
           //     SetupFlip();//MOAHAHAHAH AUTOFLIP
            }
            else
            {

                if (burgerside._BurgerCurrentHeat < burgerside._BurgerHeat)
                {//When The Heat Of The Burger Is Higher Then It Has Ever Been.
                    burgerside._BurgerCurrentHeat = burgerside._BurgerHeat;

                    if (burgerside._BurgerCurrentHeat >= BurgerIngredience.MeatTimers.colorKeys[burgerside._BurgerState].time)
                    {
                        burgerside._BurgerState++;

                        _theBurgerDrag.TheImage.sprite = BurgerIngredience.AllBurgerState[burgerside._BurgerState].IngredientSprite;
                        _theBurgerDrag.TheIngredientGameObject.ingredient = BurgerIngredience.AllBurgerState[burgerside._BurgerState];
                        //     _TheBurgerInfo.TheImage.sprite = BurgerIngredience.AllBurgerStages.GetSprite("Hamburgers_Beef_" + burgerside._BurgerState);
                   //     SetupFlip();//MOAHAHAHAH AUTOFLIP
                    }
                }
            }
        }
    }


    void EvaluateQuality()
    {//TODO make this abit more advanced later on. 

        _theBurgerDrag.TheBurgerInfos.TheQuality = 1 - Mathf.Abs(_theBurgerDrag.TheBurgerInfos.MyVariablesDown._BurgerHeat - BurgerIngredience.PerfectlyCooked) - Mathf.Abs(_theBurgerDrag.TheBurgerInfos.MyVariablesUp._BurgerHeat - BurgerIngredience.PerfectlyCooked);
        if (_theBurgerDrag.TheBurgerInfos.TheQuality < 0)
            _theBurgerDrag.TheBurgerInfos.TheQuality = 0;

    }




}
