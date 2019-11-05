using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWindowManager : MonoBehaviour // This might be a game manager thing
{

    [SerializeField] private Animator _animator;


    private bool _isOrderWindowOpen;
    private bool _isRecipeBookOpen;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void ToggleOrderWindow()
    {
        if (!_isOrderWindowOpen)
        {
            _isOrderWindowOpen = true;
            // disable other btns
            _animator.SetTrigger("FadeInOrderWindow");
        }
        else
        {
            _isOrderWindowOpen = false;
            // ENASBLE other btns
            _animator.SetTrigger("FadeOutOrderWindow");
        }
    }

    //public void ToggleRecipeBookWindow()
    //{
    //    if (!_isRecipeBookOpen)
    //    {
    //        _isOrderWindowOpen = true;
    //       _animator.SetTrigger("FadeInRecipeBookWindow");

    //    }
    //    else
    //    {
    //        _isRecipeBookOpen = false;
    //       _animator.SetTrigger("FadeOutRecipeBookWindow");

    //    }
    //}
}
