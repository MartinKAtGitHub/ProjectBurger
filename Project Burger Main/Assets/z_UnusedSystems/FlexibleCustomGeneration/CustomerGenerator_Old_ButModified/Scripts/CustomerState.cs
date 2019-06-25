using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TheCustomerStates {Walking, Talking, WaitingForOrder  }

[System.Serializable]
public class CustomerStateBehaviour {
    public Transform[] walkingPositions;
    public bool Talk = false;
    public float WaitingTime = 0;

    public CustomerStateBehaviour(Transform[] walkingPos, bool talk, float waiting) {
        walkingPositions = walkingPos;
        Talk = talk;
        WaitingTime = waiting;
    }

}
public class CustomerState : MonoBehaviour {

    [SerializeField]
    public CustomerStateBehaviour[] CustomerStates;
    public int lenght = 0;

    [SerializeField]
    private float _MoveSpeed = 1f;
    public float MoveSpeed {
        get { return _MoveSpeed; }
        set {
            if (_MoveSpeed + value < 0) {
                _MoveSpeed = 0;//Dont Want It To Go Backwards
            } else {
                _MoveSpeed = value;
            }
        }
    }

    int currentPath = 0;
    int currentBehaviour = 0;
    float TimeToWait = 0;
    int added = 0;

    bool StartCustomer = false;
    TheCustomerStates _TheState;

    private void Start() {
        lenght = CustomerStates.Length;
    }

    private void Update() {

        if (StartCustomer == true) {

            if (_TheState == TheCustomerStates.Walking) {
                if (CustomerStates[currentBehaviour].walkingPositions.Length > 0) {//If Customer Needs To Walk A Specific Route To Get To The Buying Place, This Does That

                    transform.position = Vector3.MoveTowards(transform.position, CustomerStates[currentBehaviour].walkingPositions[currentPath].position, MoveSpeed * Time.deltaTime);
                    if (transform.position == CustomerStates[currentBehaviour].walkingPositions[currentPath].position) {
                        if (currentPath < CustomerStates[currentBehaviour].walkingPositions.Length - 1) {
                            currentPath++;
                        } else {
                            _TheState = TheCustomerStates.Talking;
                        }
                    }
                } else {
                    _TheState = TheCustomerStates.Talking;
                }

            } else if (_TheState == TheCustomerStates.Talking) {

                if (CustomerStates[currentBehaviour].Talk == true) {//TODO Additional Logic Needed If Going To Work
                    TimeToWait = Time.time + CustomerStates[currentBehaviour].WaitingTime;
                    _TheState = TheCustomerStates.WaitingForOrder;
                } else {
                    TimeToWait = Time.time + CustomerStates[currentBehaviour].WaitingTime;
                    _TheState = TheCustomerStates.WaitingForOrder;
                }

            } else if (_TheState == TheCustomerStates.WaitingForOrder) {

                if (TimeToWait <= Time.time) {//Continue With Next State If Not Delete
                    if (currentBehaviour < CustomerStates.Length - 1) {
                        currentBehaviour++;
                        _TheState = TheCustomerStates.Walking;
                        currentPath = 0;
                    } else {
                        Destroy(gameObject);
                    }

                }
            }
        }

    }


    public TheCustomerStates TheCustomerState() {
        return _TheState;
    }

    public void GoToWaiting() {
        if (_TheState == TheCustomerStates.Talking) {
            _TheState = TheCustomerStates.WaitingForOrder;
        }
    }


    public void ForceLastState() {
        _TheState = TheCustomerStates.Walking;
        currentPath = 0;
        currentBehaviour = CustomerStates.Length - 1;
    }

    public void StartCustomerStates() {
        StartCustomer = true;
    }

    public void MakeNewInstance(int lengths) {
        CustomerStates = new CustomerStateBehaviour[lengths];
        added = 0;
    }
    public void SetBehaviours(Transform[] paths, bool talk, float waiting) {
        CustomerStates[added++] = new CustomerStateBehaviour(paths, talk, waiting);
    }


}
