using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSwipeSetter : MonoBehaviour {

    [SerializeField]
    private Transform _activatedTable = null;
    [SerializeField]
    private Transform _deactivatedTable = null;
    [SerializeField]
    private RectTransform _canvas = null;

    private Transform _tableSaver;
    private Vector3 _moveToPos = Vector3.zero;

    private bool _changingTable = false;
    private bool _leftRight = false;

    private int _addedAdditionalTablesToSwipe = 0;
    private int _maximumTables = 2;
    private float _time = 0;
    private float _canvasScale = 1920;



    private void Start() {
        _moveToPos = transform.localPosition;
        _maximumTables = _activatedTable.childCount + _deactivatedTable.childCount - 1;
    }

    private void Update() {

        if (_changingTable == true) {

            transform.localPosition = Vector3.Lerp(transform.localPosition, _moveToPos, _time += Time.deltaTime);

            if (_time >= 1) {
                _changingTable = false;
                Remove();
            }
        }

    }

    public void SwipedLeft() {

        if (_changingTable == false || (_changingTable == true && _leftRight == true)) {
            if (_addedAdditionalTablesToSwipe < _maximumTables) {

                _addedAdditionalTablesToSwipe++;
                _leftRight = true;
                _changingTable = true;
                _time = 0;//Resetting Time

                _tableSaver = _deactivatedTable.GetChild(_deactivatedTable.childCount - 1);//Getting Last Child Of DeactivatingTables To Activate
                _tableSaver.SetParent(_activatedTable);//Setting To ActivatedTable And Last Position

                transform.localPosition += Vector3.right * (_canvasScale / 2f);//Adding Padding To The ActiveTables GameObject To Make The Newly Activated Table Spawn Outside Of The Screen
                _moveToPos -= Vector3.right * (_canvasScale / 2f);//Setting Padding To The New Location To Travel To Dependant Of How Many Tables Are In The ActiveTable

            }
        }

    }

    public void SwipedRight() {
        if (_changingTable == false || (_changingTable == true && _leftRight == false)) {
            if (_addedAdditionalTablesToSwipe < _maximumTables) {

                _addedAdditionalTablesToSwipe++;
                _changingTable = true;
                _leftRight = false;
                _time = 0;

                _tableSaver = _deactivatedTable.GetChild(0);
                _tableSaver.SetParent(_activatedTable);
                _tableSaver.SetAsFirstSibling();//In Addition Here I Need To Set That Since Im Swiping To The Right I Want To Set This To Spawn On The Left Side == First Child

                transform.localPosition -= Vector3.right * (_canvasScale / 2f);
                _moveToPos += Vector3.right * (_canvasScale / 2f);

            }
        }

    }

   

    private void Remove() {

        for (int i = 0; i < _addedAdditionalTablesToSwipe; i++) {//Removing Objects From ActiveTables And Sending Them To DeactivatedTables

            if (_leftRight == true) {
                _tableSaver = _activatedTable.GetChild(0);
                _tableSaver.SetParent(_deactivatedTable);
                _tableSaver.SetAsFirstSibling();//Placing The Tables Which Was Swiped Left To The First Position In DeactivatedTables
            } else {
                _tableSaver = _activatedTable.GetChild(_activatedTable.childCount - 1);
                _tableSaver.SetParent(_deactivatedTable);
            }
        }

        _addedAdditionalTablesToSwipe = 0;
        _moveToPos.x = 0;
        transform.localPosition = Vector3.up * transform.localPosition.y;

    }

}
