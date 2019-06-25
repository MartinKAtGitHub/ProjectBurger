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

    int _CurrentPath = 0;
    int _CurrentBehaviour = 0;
    float _TimeToWait = 0;
    int _Adding = 0;

    bool _StartCustomer = false;
    TheCustomerStates _TheState;
 

    private void Update() {

        if (_StartCustomer == true) {

            if (_TheState == TheCustomerStates.Walking) {
                if (CustomerStates[_CurrentBehaviour].walkingPositions.Length > 0) {//If Customer Needs To Walk A Specific Route To Get To The Buying Place, This Does That

                    transform.position = Vector3.MoveTowards(transform.position, CustomerStates[_CurrentBehaviour].walkingPositions[_CurrentPath].position, MoveSpeed * Time.deltaTime);
                    if (transform.position == CustomerStates[_CurrentBehaviour].walkingPositions[_CurrentPath].position) {
                        if (_CurrentPath < CustomerStates[_CurrentBehaviour].walkingPositions.Length - 1) {
                            _CurrentPath++;
                        } else {
                            _TheState = TheCustomerStates.Talking;
                        }
                    }
                } else {
                    _TheState = TheCustomerStates.Talking;
                }

            } else if (_TheState == TheCustomerStates.Talking) {

                if (CustomerStates[_CurrentBehaviour].Talk == true) {//TODO Additional Logic Needed If Going To Work
                    _TimeToWait = Time.time + CustomerStates[_CurrentBehaviour].WaitingTime;
                    _TheState = TheCustomerStates.WaitingForOrder;
                } else {
                    _TimeToWait = Time.time + CustomerStates[_CurrentBehaviour].WaitingTime;
                    _TheState = TheCustomerStates.WaitingForOrder;
                }

            } else if (_TheState == TheCustomerStates.WaitingForOrder) {

                if (_TimeToWait <= Time.time) {//Continue With Next State If Not Delete
                    if (_CurrentBehaviour < CustomerStates.Length - 1) {
                        _CurrentBehaviour++;
                        _TheState = TheCustomerStates.Walking;
                        _CurrentPath = 0;
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
        _CurrentPath = 0;
        _CurrentBehaviour = CustomerStates.Length - 1;
    }

    public void StartCustomerStates() {
        _StartCustomer = true;
    }

    public void MakeNewInstance(int lengths) {
        CustomerStates = new CustomerStateBehaviour[lengths];
        _Adding = 0;
    }
    public void SetBehaviours(Transform[] paths, bool talk, float waiting) {
        CustomerStates[_Adding++] = new CustomerStateBehaviour(paths, talk, waiting);
    }


}
