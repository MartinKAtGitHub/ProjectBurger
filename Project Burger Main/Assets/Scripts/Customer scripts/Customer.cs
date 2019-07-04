using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour {

    [SerializeField] private int _CustomerWaitingTime;




    [SerializeField] private string _customerName;
    public string CustomerName { get => _customerName; }

    private Order _order;
    public Order Order {
        get {
            return _order;
        }
    }


    [SerializeField]
    private OrderGenerator _OrderGenerator = null;
    public OrderGenerator OrderGenerator {
        get {
            return _OrderGenerator;
        }
    }

    [SerializeField]
    private CustomerState _CustomerStates = null;
    public CustomerState CustomerStates {
        get {
            return _CustomerStates;
        }
    } 

    private void OnDestroy() {
        //Destoyed
    }

    private void OnEnable() 
    {

        if (_order != null) {
            _customerName = gameObject.name;
            _order = OrderGenerator.RequestOrder();
            _order.CustomerName = _customerName;
        }
    }

    private void Start() {
        //Start Walk
        //Stop At Disc
        //Start Dialog;

        _CustomerStates.StartCustomerStates();

    }


    void SetWaitingTime() {

    }



    public void SetCustomerStates(TheCustomSpawner spawner) {
        TransformArray2 Paths = spawner.WalkingPositions.WalkingPossibilities[0].GroupInGroups[Random.Range(0, spawner.WalkingPositions.WalkingPossibilities[0].GroupInGroups.Length)];
        _CustomerStates.MakeNewInstance(Paths.PathInGroup.Length);

        _customerName = gameObject.name;
        _order = OrderGenerator.RequestOrder();
        _order.CustomerName = _customerName;


        for (int i = 0; i < Paths.PathInGroup.Length; i++) {
            _CustomerStates.SetBehaviours(
           Paths.PathInGroup[i].PositionsInPath , Paths.PathInGroup[i].Talking, _order.OrderRecipes.Count * 25f * Paths.PathInGroup[i].Patience);
        }



    }
    
}
