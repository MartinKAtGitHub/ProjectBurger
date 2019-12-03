using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequestCard : SlotVertical
{
    [SerializeField] TextMeshProUGUI _recipeTitleTxt;
    [SerializeField] SpecialRequestElement[] _specialRequestElements;

    public TextMeshProUGUI RecipeTitleTxt { get => _recipeTitleTxt; }
    public SpecialRequestElement[] SpecialRequestElements { get => _specialRequestElements; }
}
