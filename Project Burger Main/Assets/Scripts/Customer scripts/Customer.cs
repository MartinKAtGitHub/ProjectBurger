using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderGenerator))]
public class Customer : MonoBehaviour {

    public CustomerPatience _patienceInfo;
  

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

    private void OnDestroy() {
        //Destoyed

        //TODO On Exit Shop, Give happiness Symbol?? 
        //This Should Be On Sell Button, But Later.
        //       LevelManager.Instance.ScoreManager.CalculateScoreMultipleItems(this);
        LevelManager.Instance.ScoreManager.GoldEarned += _patienceInfo.CustomerGold;
    }

    private void OnEnable() 
    {
        if (_OrderGenerator == null)
            _OrderGenerator = GetComponent<OrderGenerator>();

        if (_order == null) {
            _customerName = gameObject.name;
            _order = OrderGenerator.RequestOrder();
            _order.CustomerName = _customerName;
        }
    }

    private void Start() {

        if (_OrderGenerator == null)
            _OrderGenerator = GetComponent<OrderGenerator>();
        _patienceInfo.SetOrderPatience(_order);
    }


    void SetWaitingTime() {

    }

}
