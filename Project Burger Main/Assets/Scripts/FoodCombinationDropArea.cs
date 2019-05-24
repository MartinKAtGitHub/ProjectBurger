using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodCombinationDropArea : DropArea
{
    private Stack<Ingredient> _ingredients = new Stack<Ingredient>(); // I cant really see the stack 


    private int indexCounter;

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        var ingredient = eventData.pointerDrag.GetComponent<Ingredient>();
        if(ingredient != null)
        {
            _ingredients.Push(ingredient);
        }

        CheckFoodStackWithRecepies();
    }

    public override void OnDropAreaBeginDrag()
    {
        _ingredients.Pop();
    }

    private void AddIngredientsToFoodStack()
    {

    }
    private void CheckFoodStackWithRecepies()
    {
       

        //for (int i = 0; i < Recepiebook.length; i++)
        //{
        //    if ( FoodStack[indexCounter] == recepieBook.recepes[i].ingredient[indexCounter])
        //    {

        //        indexCounter++;
        //        return;
        //    }
        //}
        //// Error Warning

    }






    [CustomEditor(typeof(FoodCombinationDropArea))]
    public class StackPreview : Editor
    {
        public override void OnInspectorGUI()
        {
            var ts = (FoodCombinationDropArea)target;
            var stack = ts._ingredients;

            var bold = new GUIStyle();
            bold.fontStyle = FontStyle.Bold;
            GUILayout.Label("FoodStack", bold);

            foreach (var item in stack)
            {
                GUILayout.Label(item.name);
            }
        }
    }

}
