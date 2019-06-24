using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerRefrences : MonoBehaviour {

    public CustomerTorsoSprites TorsoSprites;
    public CustomerHeadSprites HeadSprites;

    [Space(10)]
    public RectTransform TorsoRectTransform;
    public RectTransform HeadRectTransform;

    [Space(10)]//Going To Add More In THe Future Probably
    public Image TorsoRef;
    public Image HeadRef;
    public Image EyeRef;
    public Image NoseRef;
    public Image MouthRef;
    public Image HairRef;

    public RecipeBook RecipeB;
    public int MultiorderAmount;
    public int MultiorderChance;
}