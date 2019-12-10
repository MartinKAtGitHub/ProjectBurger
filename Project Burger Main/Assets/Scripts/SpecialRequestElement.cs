using UnityEngine;
using UnityEngine.UI;

public class SpecialRequestElement : MonoBehaviour
{

    [SerializeField] private Sprite _addExtraIcon;
    [SerializeField] private Sprite _removeExtraIcon;

    [SerializeField] private Image _ingredientIconImg;
    [SerializeField] private Image _removeAddCheckMarkIconImg;

    private void Awake()
    {
        if(_ingredientIconImg == null)
        {
            Debug.LogError("Missing ingredientIconImg, drag and drop");
        }
        if (_removeAddCheckMarkIconImg == null)
        {
            Debug.LogError("Missing removeAddCheckMarkIconImg, drag and drop");
        }
    }



}
