using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BurgerMeatLogic : MonoBehaviour, IPointerClickHandler
{

    private Animator _animator;
    private TheGrill _theGrill;
    private Image _ingredientImg;

    private bool _burgerSide = true;
    private float _currentTimeUsedOnTop = 0;
    private float _currentTimeUsedOnBot = 0;
    private int _burgerStateTop = 0;
    private int _burgerStateBot = 0;
    private bool _burnt = false;

    public float TimeToBeDoneTop;
    public float TimeToBeDoneBot;

    [SerializeField] private Sprite[] _burgerStatesSprites;
    public TheGrill TheGrill { get => _theGrill; set => _theGrill = value; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _ingredientImg = GetComponentInChildren<Image>();
        

    }

    private void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (TheGrill != null)
        {
            Flip();
        }
        else
        {
            Debug.Log("Cant flip burger, Not on grill");
        }
    }


    private void Flip()
    {


        if (_burnt == false)
        {

            //TODO flip resets teperature to closest state temperature point.
            if (_burgerSide == true)
            {
                _currentTimeUsedOnTop = TimeToBeDoneTop * (0.5f * _burgerStateTop);
            }
            else
            {
                _currentTimeUsedOnBot = TimeToBeDoneBot * (0.5f * _burgerStateBot);
            }


            _burgerSide = !_burgerSide;

            if (_burgerStateBot == 0 && _burgerStateTop == 0)
            {//flip raw burger.
                _animator.SetInteger(0, 0);
            }

            if (_burgerStateBot == 1 && _burgerStateTop == 0)
            {//flip bot to top.
                _animator.SetInteger(0, 1);
            }
            else if (_burgerStateBot == 0 && _burgerStateTop == 1)
            {//flip top to bot
                _animator.SetInteger(0, 2);
            }
            else if (_burgerStateBot == 1 && _burgerStateTop == 1)
            {//flip ready burger
                _animator.SetInteger(0, 3);
            }

            _animator.SetTrigger("Flip");
            TheGrill.flipped();//TODO This Makes The Burger Send Temperatures To The Burger, So There Needs To Be A Delay To This Method

        }
    }

    public void AddHeatToBurger(float value)
    {

        if (_burnt == false)
        {
            if (_burgerSide == true)
            {
                _currentTimeUsedOnTop += value;
                if (_currentTimeUsedOnTop / TimeToBeDoneTop > ((1 + _burgerStateTop) * 0.5f))
                {//if burger is 50% complete, then its done.    since we have raw, done, burnt. then raw is from 0-50% done is from 50-100% and burnt is after. adding more states would mean that the 0.5f is decreased to a lower value. if we have 5 states then each value would be increased with 0.25f
                    _burgerStateTop++;
                    if (_burgerStateTop >= 1) // adding +1 cuz there is two stages that the burger can be in at one point, top raw
                    {
                        _ingredientImg.sprite = _burgerStatesSprites[_burgerStateTop + 1];
                    }
                    else
                    {
                        _ingredientImg.sprite = _burgerStatesSprites[_burgerStateTop];
                    }
                    if (_burgerStateTop >= 3)
                    {
                        _burnt = true;
                        _burgerStateBot = 3;
                        _burgerStateTop = 3;
                    }
                }
            }
            else
            {
                _currentTimeUsedOnBot += value;
                if (_currentTimeUsedOnBot / TimeToBeDoneBot > ((1 + _burgerStateBot) * 0.5f))
                {//if burger is 50% complete, then its done.
                    _burgerStateBot++;
                    if (_burgerStateBot > 1)
                    {
                        _ingredientImg.sprite = _burgerStatesSprites[_burgerStateBot + 1];
                    }
                    else
                    {
                        _ingredientImg.sprite = _burgerStatesSprites[_burgerStateBot];
                    }

                    if (_burgerStateBot >= 3)
                    {
                        _burnt = true;
                        _burgerStateBot = 3;
                        _burgerStateTop = 3;
                    }
                }
            }
        }
    }
}
