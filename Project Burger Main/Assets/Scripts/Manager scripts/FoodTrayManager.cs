using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrayManager : MonoBehaviour
{
    [SerializeField] private GameObject _foodTrayPrefab;

    private List<FoodTray> _foodTrays = new List<FoodTray>();

    private int _currentIndex;

    // private  FoodTray[] _foodTrays;
    private void Awake()
    {
        LevelManager.Instance.FoodTrayManger = this;
        _currentIndex = 0;
    }

    private void Start()
    {
        SpawnFoodtrays();
        InitializeFoodTray();
    }

    private void SpawnFoodtrays()
    {
        Debug.Log("Spawning inn trays");
        var offset = _foodTrayPrefab.GetComponent<RectTransform>().sizeDelta.y * -1;

        for (int i = 0; i < LevelManager.Instance.CustomerSelectSwiper.QueueSlots.Length; i++)
        {
            var tray = Instantiate(_foodTrayPrefab, transform, false);

            tray.name = name + i;
            tray.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offset);
            tray.gameObject.SetActive(false);
            _foodTrays.Add(tray.GetComponent<FoodTray>());

        }
    }

    private void InitializeFoodTray()
    {
        _foodTrays[_currentIndex].gameObject.SetActive(true);
    }


    public void SetFoodTrayFocus(int newIndex)
    {
        if (_currentIndex != newIndex)
        {
            _foodTrays[_currentIndex].gameObject.SetActive(false);
            _foodTrays[newIndex].gameObject.SetActive(true);

            _currentIndex = newIndex;
        }
    }
}
