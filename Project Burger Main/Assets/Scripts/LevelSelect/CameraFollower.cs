using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public Transform MapCentert;

    public Vector2 MapCenter = Vector2.zero;
    public Vector2 MapCenterOffset = Vector2.zero;

    public Vector3 GoToSpot = Vector2.zero;

    public Transform Player;

    public bool UpdateCamera = false;
    public bool PlayerRunning = false;
    public bool PlayerCompleteWalking = false;

    public float Speed = 1;

    private void Start() {
        transform.position = Player.transform.position - (Vector3.forward * 10);
        MapCenter = MapCentert.position;
        GoToSpot.z = -10;
    }

    void Update() {

        if(PlayerRunning == true) {

            if (Player.position.x < MapCenter.x - MapCenterOffset.x) {
            } else if (Player.position.x > MapCenter.x + MapCenterOffset.x) {
            } else {
                UpdateCamera = true;
                PlayerRunning = false;
            }

            if (Player.position.y < MapCenter.y - MapCenterOffset.y) {
            } else if (Player.position.y > MapCenter.y + MapCenterOffset.y) {
            } else {
                UpdateCamera = true;
            PlayerRunning = false;
            }


        }

        if (UpdateCamera == true) {

            if (PlayerCompleteWalking == false) {

                if (Player.position.x < MapCenter.x - MapCenterOffset.x) {  
                    GoToSpot.x = MapCenter.x - MapCenterOffset.x;
                } else if (Player.position.x > MapCenter.x + MapCenterOffset.x) {
                    GoToSpot.x = MapCenter.x + MapCenterOffset.x;
                } else {
                    GoToSpot.x = Player.position.x;
                }

                if (Player.position.y < MapCenter.y - MapCenterOffset.y) {
                    GoToSpot.y = MapCenter.y - MapCenterOffset.y;
                } else if (Player.position.y > MapCenter.y + MapCenterOffset.y) {
                    GoToSpot.y = MapCenter.y + MapCenterOffset.y;
                } else {
                    GoToSpot.y = Player.position.y;
                }

            }

           transform.position = Vector3.MoveTowards(transform.position, GoToSpot, 1 * Time.deltaTime * (Speed * Vector3.Distance(transform.position, GoToSpot)));

            if (PlayerCompleteWalking == true && Vector3.Distance(transform.position, GoToSpot) <= 0.01) {
                UpdateCamera = false;
            }

        }
        
    }

    public void CameraFollow() {
        PlayerRunning = true;
        PlayerCompleteWalking = false;
    }

    public void CompletedWalk() {
        PlayerCompleteWalking = true;
    }


}
