﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public QueueManager QueueManager;
    //public CustomerSelect CustomerSelect;
    public FoodTray TESTING_FoodTray;
    //public CustomerSpawner CustomerSpawner;
    public SalesManager SalesManager;
    public ShuffleBag ShuffleBag;
    public ScoreManager ScoreManager;
    // public LimitedCustomerSelect CustomerSelect;
    public CustomerSelectSwiper CustomerSelectSwiper;
    public OrderWindow OrderWindow;
    public WinLooseManager WinLooseManager;
    public TakeOrder TakeOrder;

     private FoodTrayManager _foodTrayManger;


    [SerializeField] private float _preparationTime;
    [SerializeField] private Vector2 _customerSpawnTimerMinMax;
    [SerializeField] private bool _enableSpawner;

    public static LevelManager Instance { get; private set; }
    public FoodTrayManager FoodTrayManger { get => _foodTrayManger; set => _foodTrayManger = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Found another LevelManager in the same Scene, make sure only 1 LevelManager exist per scene");
            Destroy(gameObject); // Destroy myself is Instance already has a ref
        }

    }

    void Start()
    {
        NullCheck<QueueManager>(QueueManager);
        // NullCheck<FoodTrayManager>(FoodTrayManger);
        // NullCheck(FoodTray);
        // NullCheck(CustomerSpawner);
        NullCheck(SalesManager);
        NullCheck(ShuffleBag);
        NullCheck(ScoreManager);
        // NullCheck(CustomerSelect);
        NullCheck(OrderWindow);
        NullCheck(WinLooseManager);
        NullCheck(TakeOrder);

        if (_enableSpawner)
        {
            StartCoroutine(CustomerSpawnSystemInit());

        }


    }

    /// <summary>
    /// Starts the customer spawn system, we need a special case for the first customer that spawns which only happens once.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CustomerSpawnSystemInit()
    {
        yield return new WaitForSeconds(_preparationTime);

        var customer = ShuffleBag.Next();
        QueueManager.AddCustomerToQueue(customer);
        // CustomerSelect.ZeroIndexCustomer();

        yield return new WaitForSeconds(3);
        StartCoroutine(CustomerSpawnSystemLoop());
    }
    /// <summary>
    /// This is th main customer spawn loop. At the end of the list this method will be called again.
    /// </summary>
    private IEnumerator CustomerSpawnSystemLoop()
    {

        //for (int i = 0; i < ShuffleBag.CustomerPool.Count; i++) // This over shoots the Limit
        while (ShuffleBag.MaxCount > 0)
        {
            yield return new WaitUntil(() =>
            {
                //  return QueueManager.ActiveCustomerQueue.Count < QueueManager.ActiveQueueLimit;
                return QueueManager.NumOfActiveCustomers < QueueManager.ActiveQueueLimit;


            }); // PERFORMANCE Levelmanager.cs | StartLevel() The bool is checked every frame, until it turns true

            yield return new WaitForSeconds(Random.Range(_customerSpawnTimerMinMax.x, _customerSpawnTimerMinMax.y));

            var customer = ShuffleBag.Next();
            QueueManager.AddCustomerToQueue(customer);
            //    counter++;
            // Debug.Log("Count = " + counter);
            yield return null;
        }
        // Signal end of loop, maybe have an event and fire this metod again
        // OR StartCoroutine(CustomerSpawnSystemLoop()); call it again, might crash 
    }


    private void NullChecking(object componant)
    {
        if (componant == null)
        {
            Debug.LogError($"Component {nameof(componant)} is NULL");
        }
    }

    private void NullCheck<T>(T reference)
    {
        if (reference == null)
        {
            Debug.LogError(/*nameof(reference)+*/ "  missing reference -> check below console for Line Num");
        }
    }
}
