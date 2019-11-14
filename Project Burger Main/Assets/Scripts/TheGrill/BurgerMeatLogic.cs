using UnityEngine;
using UnityEngine.EventSystems;

public class BurgerMeatLogic : MonoBehaviour, IPointerClickHandler  {

    private Animator _animator;
    private TheGrill _theGrill;
    private enum BurgerSides
    {
        Top,
        Bot
    }

    private bool _burgerSide = true;
    private float _currentTimeUsedOnTop = 0;
    private float _currentTimeUsedOnBot = 0;
    private float _burgerStateTop = 0;
    private float _burgerStateBot = 0;

    public float TimeToBeDoneTop;
    public float TimeToBeDoneBot;

    public TheGrill TheGrill { get => _theGrill; set => _theGrill = value; }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Flip()
    {
        Debug.Log("Flipping burger");

        _burgerSide = !_burgerSide;
        // player anim

        if(_burgerSide == true) {//TODO When Flip Change Sprite Of The Burger
            //BurgerSideTop == Sprite[_burgerStateTop];
        } else {
            //BurgerSideBot == Sprite[_burgerStateBot];
        }

        TheGrill.flipped();//TODO This Makes The Burger Send Temperatures To The Burger, So There Needs To Be A Delay To This Method
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(TheGrill != null)
        {
            Flip();
        }
        else
        {
            Debug.Log("Cant flip burger, Not on grill");
        }
    }

    public void AddHeatToBurger(float value) 
    {
        if(_burgerSide == true) {
            _currentTimeUsedOnTop += value;
            if (_currentTimeUsedOnTop / TimeToBeDoneTop > ((1 + _burgerStateTop) * 0.5f)) {//if burger is 50% complete, then its done.    since we have raw, done, burnt. then raw is from 0-50% done is from 50-100% and burnt is after. adding more states would mean that the 0.5f is decreased to a lower value. if we have 5 states then each value would be increased with 0.25f
                _burgerStateTop++;
                //TODO CHANGE BURGER SPRITE  //State 0 == raw. 1 == done. 2 == burnt
                //BurgerSideTop == Sprite[_burgerStateTop];
            }
        } else {
            _currentTimeUsedOnBot += value;
            if (_currentTimeUsedOnBot / TimeToBeDoneBot > ((1 + _burgerStateBot) * 0.5f)) {//if burger is 50% complete, then its done.
                _burgerStateBot++;
                //TODO CHANGE BURGER SPRITE
                //BurgerSideBot == Sprite[_burgerStateBot];
            }
        }

    }
}
