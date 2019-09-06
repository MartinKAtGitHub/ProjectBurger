﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    [SerializeField]
    private float _speed = 1;

    public float MapXLeft = 0;
    public float MapXRight = 0;
    public float MapYDown = 0;
    public float MapYUp = 0;

    [SerializeField]
    private Vector3 GoToSpot = Vector3.zero;

    [SerializeField]
    private Transform _player = null;


    [SerializeField]
    private bool _updateCamera = false;
    [SerializeField]
    private bool _playerRunning = false;


    private void Start() {
        GoToSpot.z = -10;

        MapXLeft = GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.CameraOffsecLeftX;
        MapXRight = GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.CameraOffsecRightX;

        MapYDown = GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.CameraOffsecDownY;
        MapYUp = GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.CameraOffsecUpY;

    }

    public void StartCamera() {
        CheckPosition();
        transform.position = GoToSpot;
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
    public void UpdateAreaOffset() {  
        UpdateMapOffsetValues();
        CheckPosition();
        _updateCamera = true;

    }

    void UpdateMapOffsetValues() {

        MapXRight = GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.CameraOffsecRightX;
        MapXLeft = GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.CameraOffsecLeftX;

        MapYDown = GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.CameraOffsecDownY;
        MapYUp = GameInfoHolder.Instance.TheSaveFile.LevelSelectData.PlayerData.CameraOffsecUpY;

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
