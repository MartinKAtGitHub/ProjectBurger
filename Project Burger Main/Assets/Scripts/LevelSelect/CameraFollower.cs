using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    [SerializeField]
    private float _speed = 1;

    private float MapXLeft = 0;
    private float MapXRight = 0;
    private float MapYDown = 0;
    private float MapYUp = 0;

    [SerializeField]
    private Vector3 GoToSpot = Vector3.zero;

    [SerializeField]
    private Transform _player = null;
    [SerializeField]
    private AreaPoints _points = null;

    [SerializeField]
    private bool _updateCamera = false;
    [SerializeField]
    private bool _playerRunning = false;


    private void Awake() {
        GoToSpot.z = -10;
    }



    void Update() {

        if (_playerRunning == true) {
            if (_player.position.x > MapXLeft && _player.position.x < MapXRight) {//Update Camera Positions If Player Is Withing The Updating Positions.
                _updateCamera = true;

                if (_player.position.x < MapXLeft) {
                    GoToSpot.x = MapXLeft;
                } else if (_player.position.x > MapXRight) {
                    GoToSpot.x = MapXRight;
                } else {
                    GoToSpot.x = _player.position.x;
                }
            }

            if (_player.position.y > MapYDown && _player.position.y < MapYUp) {//Update Camera Positions If Player Is Withing The Updating Positions.
                _updateCamera = true;

                if (_player.position.y < MapYDown) {
                    GoToSpot.y = MapYDown;
                } else if (_player.position.y > MapYUp) {
                    GoToSpot.y = MapYUp;
                } else {
                    GoToSpot.y = _player.position.y;
                }

            }

        }

        if (_updateCamera == true) {

            transform.position = Vector3.MoveTowards(transform.position, GoToSpot, 1 * Time.deltaTime * (_speed * Vector3.Distance(transform.position, GoToSpot)));

            if (_playerRunning == false && Vector3.Distance(transform.position, GoToSpot) <= 0.01) {
                transform.position = GoToSpot;
                _updateCamera = false;
            }

        }

    }

    public void CameraFollow() {
        _playerRunning = true;

    }

    public void CompletedWalk() {//Doing The Final Update To The Camera Cuz Player Finished
        _playerRunning = false;
        CheckPosition();

    }

    /// <summary>
    /// Changes The Offset Values And Updates Which AreaPoints Are Active
    /// </summary>
    /// <param name="points">Offset Points</param>
    public void UpdateAreaOffset(AreaPoints points) {
        _points = points;
        GameInfoHolder.Instance.SetCameraPoint(points);
        UpdateMapOffsetValues();
        CheckPosition();
        _updateCamera = true;

    }

    /// <summary>
    /// Changes The Offset Values And Updates Which AreaPoints Are Active. And Updates The Camera Position To The Player
    /// </summary>
    /// <param name="points"></param>
    public void UpdateAndApplyAreaOffsetValues(AreaPoints points) {
        _points = points;
        UpdateMapOffsetValues();
        CheckPosition();

        transform.position = GoToSpot;
        _updateCamera = false;

    }

    public void SetStartValues() {
        UpdateMapOffsetValues();
        CheckPosition();
        transform.position = GoToSpot;
        _updateCamera = false;

    }


    void UpdateMapOffsetValues() {
        MapXLeft = _points.AreaOffsetX.x;
        MapXRight = _points.AreaOffsetX.y;

        MapYDown = _points.AreaOffsetY.x;
        MapYUp = _points.AreaOffsetY.y;

    }

    void CheckPosition() {
        if (_player.position.x < MapXLeft) {
            GoToSpot.x = MapXLeft;
        } else if (_player.position.x > MapXRight) {
            GoToSpot.x = MapXRight;
        } else {
            GoToSpot.x = _player.position.x;
        }

        if (_player.position.y < MapYDown) {
            GoToSpot.y = MapYDown;
        } else if (_player.position.y > MapYUp) {
            GoToSpot.y = MapYUp;
        } else {
            GoToSpot.y = _player.position.y;
        }

    }

}
