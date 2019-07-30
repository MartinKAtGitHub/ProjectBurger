using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerGeneratorTest : MonoBehaviour
{
    public GameObject FoodGameobject;
    public Sprite[] solidingredientsprites;
    public Sprite BotBunsprite;
    public Sprite TopBunsprite;

    public float IngredientOffsetValue;
    public float FoodGOOffsetValueX;
    public float FoodGOOffsetValueY;

    public int[] BurgerSize;
    public int BurgersPerRow;
    public int BurgerRows;

    [Space(25)]
    public Sprite[] BunsLayerImg;
    public Sprite[] MeatLayerImg;
    public Sprite[] CheeseLayerImg;
    public Sprite[] SaladLayerImg;
    public Sprite[] TopLayerImg;

    public int[] MaxLayerBugerSize;
    void Start()
    {
        //GenereateSolidOffsetBurgers();
       // GenerateLayerdBurgers();
    }


    private void GenerateLayerdBurgers()
    {
        for (int i = 0; i < BurgerRows; i++)
        {

            for (int j = 0; j < BurgersPerRow; j++)
            {
                var foodGO = Instantiate(FoodGameobject, this.transform);
                foodGO.transform.localPosition = new Vector3(foodGO.transform.localPosition.x + FoodGOOffsetValueX * j, foodGO.transform.localPosition.x + FoodGOOffsetValueY * i);

                var bSize = MaxLayerBugerSize[Random.Range(0, MaxLayerBugerSize.Length)];
                for (int k = 0; k < bSize; k++)
                {
                    if (k == 0) // Bot bun start
                    {
                        var botBun = new GameObject("Botbun");
                        botBun.transform.SetParent(foodGO.transform);
                        botBun.transform.localPosition = Vector3.zero;
                        botBun.transform.localPosition = new Vector3(0, botBun.transform.localPosition.y);
                        botBun.transform.localScale = Vector3.one;
                        var botimg = botBun.AddComponent<Image>();
                        botimg.sprite = BunsLayerImg[k];
                        continue;
                    }
                    if (k == bSize - 1) // top bun end
                    {
                        var TopBun = new GameObject("Topbun");
                        TopBun.transform.SetParent(foodGO.transform);
                        TopBun.transform.localPosition = Vector3.zero;
                        TopBun.transform.localPosition = new Vector3(0, TopBun.transform.localPosition.y );
                        TopBun.transform.localScale = Vector3.one;
                        var topimg = TopBun.AddComponent<Image>();
                        topimg.sprite = TopLayerImg[k];
                        break;
                    }

                    var ingredient = new GameObject();
                    ingredient.transform.SetParent(foodGO.transform);
                    ingredient.transform.localPosition = Vector3.zero;
                    ingredient.transform.localPosition = new Vector3(0, ingredient.transform.localPosition.y );
                    ingredient.transform.localScale = Vector3.one;
                    var img = ingredient.AddComponent<Image>();


                    switch (Random.Range(0,3))
                    {
                        //case 0:
                        //    img.sprite = BunsLayerImg[k];
                        //    ingredient.name = BunsLayerImg[k].name;
                        //    break;
                        case 0:
                            img.sprite = MeatLayerImg[k];
                            ingredient.name = MeatLayerImg[k].name;
                            break;
                        case 1:
                            img.sprite = CheeseLayerImg[k];
                            ingredient.name = CheeseLayerImg[k].name;
                            break;
                        case 2:
                            img.sprite = SaladLayerImg[k];
                            ingredient.name = SaladLayerImg[k].name;
                            break;

                        default:
                            break;
                    }
                   
                  
                }
            }
        }
    }

    private void GenereateSolidOffsetBurgers()
    {


        for (int i = 0; i < BurgerRows; i++)
        {

            for (int j = 0; j < BurgersPerRow; j++)
            {
                var foodGO = Instantiate(FoodGameobject, this.transform);
                foodGO.transform.localPosition = new Vector3(foodGO.transform.localPosition.x + FoodGOOffsetValueX * j, foodGO.transform.localPosition.x + FoodGOOffsetValueY * i);

                var bSize = BurgerSize[Random.Range(0, BurgerSize.Length)];
                for (int k = 0; k < bSize; k++)
                {
                    if (k == 0)
                    {
                        var botBun = new GameObject("Botbun");
                        botBun.transform.SetParent(foodGO.transform);
                        botBun.transform.localPosition = Vector3.zero;
                        botBun.transform.localPosition = new Vector3(0, botBun.transform.localPosition.y + IngredientOffsetValue * k);
                        botBun.transform.localScale = Vector3.one;
                        var botimg = botBun.AddComponent<Image>();
                        botimg.sprite = BotBunsprite;
                        continue;
                    }
                    if (k == bSize - 1)
                    {
                        var TopBun = new GameObject("Topbun");
                        TopBun.transform.SetParent(foodGO.transform);
                        TopBun.transform.localPosition = Vector3.zero;
                        TopBun.transform.localPosition = new Vector3(0, TopBun.transform.localPosition.y + IngredientOffsetValue * k);
                        TopBun.transform.localScale = Vector3.one;
                        var topimg = TopBun.AddComponent<Image>();
                        topimg.sprite = TopBunsprite;
                        break;
                    }

                    var ingredient = new GameObject();
                    ingredient.transform.SetParent(foodGO.transform);
                    ingredient.transform.localPosition = Vector3.zero;
                    ingredient.transform.localPosition = new Vector3(0, ingredient.transform.localPosition.y + IngredientOffsetValue * k);
                    ingredient.transform.localScale = Vector3.one;
                    var img = ingredient.AddComponent<Image>();
                    var sprite = solidingredientsprites[Random.Range(0, solidingredientsprites.Length)];
                    img.sprite = sprite;
                    ingredient.name = sprite.name;
                }
            }
        }

    }

}
