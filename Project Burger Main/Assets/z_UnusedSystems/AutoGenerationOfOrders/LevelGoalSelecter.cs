using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoalSelecter : MonoBehaviour {

    public float goldToGet = 2000;
    public float timeLimitInSecond = 2000;
    public List<MenuItems> recipeListGenerated;
    
    [SerializeField]
    private Meny _menu;

    [SerializeField]
    private int _reachedHardLimit = 20;//HardLimit Is So That If We Have Several Items That Are Cheap, Then We Arent Flodded With 100 Small Fries In A Single Level.
    
    [SerializeField]
    private float _untillReachedAmount = 0.2f;//This Is the Percentage To Reach On Gold Or Time To Exit This Method.

    private int _previousAddedGoldIndex = 0;
    private int _previousAddedTimeIndex = 0;
    private MenuItems _previouslyaddeditem;

    private int _indexHolder = 0;
    private int _RecipeIndex = 0;
    private int _random = 0;
    private float _currentGold = 10;
    private float _currentTime = 10;
    private float _saver1 = 0;
    private float _saver2 = 0;
    private float _closestHolder = 0;
    

    private void Awake() {
        _menu.SetItems(goldToGet, timeLimitInSecond);
    }

    private void Start() {
        _previousAddedGoldIndex = _menu.RecipesGoldSorted.Length / 2;//Setting The Index Holders To Be In The Middle Of The List At Default.
        _previousAddedTimeIndex = _menu.RecipesTimeSorted.Length / 2;

        AddNewRecipesAtTheStart();//Adding Every New Recipe Once + Some Random.
        AddRecipesAtTheStart();//Adding Any Recipe At Random Untill A Criteria Is Met.

        AddingBadLuckProtectionRecipes();//Cuz The Recipes Are Chosen Somewhat Randomly, I Added This Method To Add The Least Picked Recipe.
        AddingBadLuckProtectionRecipes();

        while((_currentGold / goldToGet) < 1 || (_currentTime / timeLimitInSecond) < 1) {
            SelectClosestItem();
        }

    }

    void SelectClosestItem() {//Adding Recipe To The RecipeList.

        if ((_currentGold / goldToGet) < (_currentTime / timeLimitInSecond)) { //Finding A Recipe That Is Getting Gold On Par With Time, Cuz Gold Is Less.
            _saver1 = (_menu.RecipesGoldSorted[0].cost + _currentGold) / goldToGet;
            _saver2 = (_menu.RecipesGoldSorted[0].theTime + _currentTime) / timeLimitInSecond;
            _closestHolder = _saver1 - _saver2;

            for (int i = 0; i < _menu.RecipesGoldSorted.Length; i++) {//Finding The Recipe That Have the Value Closest To 0.
                _saver1 = (_menu.RecipesGoldSorted[i].cost + _currentGold) / goldToGet;
                _saver2 = (_menu.RecipesGoldSorted[i].theTime + _currentTime) / timeLimitInSecond;

                if (_closestHolder < _saver1 - _saver2) {//Closest To 0, But Not Over.
                    if (_closestHolder < 0) {
                        _closestHolder = (_saver1 - _saver2);
                        _indexHolder = i;
                    }
                }
            }

            if (_previouslyaddeditem == _menu.RecipesGoldSorted[_indexHolder] || _indexHolder == _previousAddedGoldIndex) {//If The Same Food Is Being Added Again, Find A New One (Just Doing A Fancy +- 1 Atm) 
                if (_indexHolder == _menu.RecipesGoldSorted.Length) {
                    _indexHolder = Random.Range(0, _menu.RecipesGoldSorted.Length - 1);
                } else if (_indexHolder == 0) {
                    _indexHolder = Random.Range(1, _menu.RecipesGoldSorted.Length);
                } else {
                    _indexHolder = Random.Range(0, _menu.RecipesGoldSorted.Length);
                }
            }

            if (_indexHolder < _previousAddedGoldIndex) {
                _previousAddedGoldIndex--;
                if (_previousAddedGoldIndex < 0)
                    _previousAddedGoldIndex = 1;
            } else if (_indexHolder > _previousAddedGoldIndex) {
                _previousAddedGoldIndex++;
                if (_previousAddedGoldIndex >= _menu.RecipesGoldSorted.Length)
                    _previousAddedGoldIndex -= 2;
            } else {
                _previousAddedGoldIndex = _indexHolder;
            }

            _previouslyaddeditem = _menu.RecipesGoldSorted[_previousAddedGoldIndex];

            _menu.RecipesGoldSorted[_previousAddedGoldIndex].used++;
            recipeListGenerated.Add(_menu.RecipesGoldSorted[_previousAddedGoldIndex]);
            _currentGold += _menu.RecipesGoldSorted[_previousAddedGoldIndex].cost;
            _currentTime += _menu.RecipesGoldSorted[_previousAddedGoldIndex].theTime;

        } else {//Finding A Recipe That Focuses On Getting Time On Par With Gold, In Percentage.
            _saver1 = (_menu.RecipesTimeSorted[0].theTime + _currentTime) / timeLimitInSecond;
            _saver2 = (_menu.RecipesTimeSorted[0].cost + _currentGold) / goldToGet;
            _closestHolder = _saver1 - _saver2;

            for (int i = 0; i < _menu.RecipesTimeSorted.Length; i++) {//Finding The Recipe That Have The Value Closest To 0. 
                _saver1 = (_menu.RecipesTimeSorted[i].theTime + _currentTime) / timeLimitInSecond;
                _saver2 = (_menu.RecipesTimeSorted[i].cost + _currentGold) / goldToGet;

                if (_closestHolder < ((_saver1 - _saver2))) {//The Closer To 0 The Better, But Dont Go Over.
                    if(_closestHolder < 0) {
                        _closestHolder = (_saver1 - _saver2);
                        _indexHolder = i;
                    }
                }

            }

            if (_previouslyaddeditem == _menu.RecipesTimeSorted[_indexHolder] || _indexHolder == _previousAddedTimeIndex) {//If The Same Food Is Being Added Again, Find A New One (Just Doing A Fancy +- 1 Atm). 
                if (_indexHolder == _menu.RecipesTimeSorted.Length) {
                    _indexHolder = Random.Range(0, _menu.RecipesTimeSorted.Length - 1);
                } else if (_indexHolder == 0) {
                    _indexHolder = Random.Range(1, _menu.RecipesTimeSorted.Length);
                } else {
                    _indexHolder = Random.Range(0, _menu.RecipesTimeSorted.Length);
                }
            }

            if (_indexHolder < _previousAddedTimeIndex) {//To Make A More Dynamic List, I Will Only Let The Adding Index Move 1 Index At A Time.
                _previousAddedTimeIndex--;
                if (_previousAddedTimeIndex < 0)
                    _previousAddedTimeIndex = 1;
            } else if (_indexHolder > _previousAddedTimeIndex) {
                _previousAddedTimeIndex++;
                if (_previousAddedTimeIndex >= _menu.RecipesTimeSorted.Length)
                    _previousAddedTimeIndex -= 2;
            } else {
                _previousAddedTimeIndex = _indexHolder;
            }
            _previouslyaddeditem = _menu.RecipesTimeSorted[_previousAddedTimeIndex];

            _menu.RecipesTimeSorted[_previousAddedTimeIndex].used++;
            recipeListGenerated.Add(_menu.RecipesTimeSorted[_previousAddedTimeIndex]);
            _currentGold += _menu.RecipesTimeSorted[_previousAddedTimeIndex].cost;
            _currentTime += _menu.RecipesTimeSorted[_previousAddedTimeIndex].theTime;
        }

    }

    void AddingBadLuckProtectionRecipes() {//Adding The Recipe That Is The Least Represented Recipe In The List.

        _saver1 = _menu._AllRecipesHolder[0].used;
        _indexHolder = 0;

        for (int i = 0; i < _menu._AllRecipesHolder.Length; i++) {

            if (_menu._AllRecipesHolder[i].used < _saver1) {
                _saver1 = _menu._AllRecipesHolder[i].used;
                _indexHolder = i;
            }
        }

        _currentGold += _menu._AllRecipesHolder[_indexHolder].cost;
        _currentTime += _menu._AllRecipesHolder[_indexHolder].theTime;
        _currentGold += _menu._AllRecipesHolder[_indexHolder].cost;
        _currentTime += _menu._AllRecipesHolder[_indexHolder].theTime;
        _menu._AllRecipesHolder[_indexHolder].used += 2;

    }

    void AddNewRecipesAtTheStart() {

        for (int j = 0; j < _menu._NewRecipesHolder.Length; j++) {//Adding One Of Each Of The New Recipes On This Level.
            recipeListGenerated.Add(_menu._NewRecipesHolder[j]);
            _currentGold += _menu._NewRecipesHolder[j].cost;
            _currentTime += _menu._NewRecipesHolder[j].theTime;
            _menu._NewRecipesHolder[j].used++;
        }
        if (_currentTime / timeLimitInSecond > _untillReachedAmount || _currentTime / goldToGet > _untillReachedAmount) //Check If Limit Is Reached To Start Creating The List.
            return;

        for (int i = 0; i < 10000; i++) {// "Infinite" Loop (Kinda). Adding Random Recipe From NewRecipes
            _random = Random.Range(0, _menu._NewRecipesHolder.Length);

            recipeListGenerated.Add(_menu._NewRecipesHolder[_random]);
            _currentGold += _menu._NewRecipesHolder[_random].cost;
            _currentTime += _menu._NewRecipesHolder[_random].theTime;
            _menu._NewRecipesHolder[_random].used++;

            if (_currentTime / timeLimitInSecond > _untillReachedAmount || _currentTime / goldToGet > _untillReachedAmount || recipeListGenerated.Count >= _reachedHardLimit) //Check If Any Of The Criteria Is Met To Stop The Adding Of Items.
                return;

        }

    }

    void AddRecipesAtTheStart() {
        _reachedHardLimit = recipeListGenerated.Count + 20;
        _untillReachedAmount = 0.5f;

        for (int i = 0; i < 10000; i++) {// "Infinite" Loop (Kinda). Adding Random Recipe From AllRecipes
            _random = Random.Range(0, _menu._AllRecipesHolder.Length);

            recipeListGenerated.Add(_menu._AllRecipesHolder[_random]);
            _currentGold += _menu._AllRecipesHolder[_random].cost;
            _currentTime += _menu._AllRecipesHolder[_random].theTime;
            _menu._AllRecipesHolder[_random].used++;

            if (_currentTime / timeLimitInSecond > _untillReachedAmount || _currentTime / goldToGet > _untillReachedAmount || recipeListGenerated.Count >= _reachedHardLimit) //Check If Any Of The Criteria Is Met To Stop The Adding Of Items.
                return;

        }

    }

    public Recipe GetOrder() {//This Get Will Give The Next One Every Time Then When It Have Given The Last Recipe, It Will Reset And Give The First One Again.
        if (_RecipeIndex >= recipeListGenerated.Count)
            _RecipeIndex = 0;
        return recipeListGenerated[_RecipeIndex++].theRecipe;
    }

}
