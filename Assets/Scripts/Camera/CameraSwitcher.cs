using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour{

    [SerializeField] private PlayerMovement playerRef;
    [SerializeField] private CinemachineVirtualCamera activeCam;


    /// OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other){

        if(!playerRef.camCon1 && other.CompareTag("Player")){
            playerRef.camCon1 = true;
        }
    }


    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
    void OnTriggerStay(Collider other){

        if( playerRef.camCon1 && !playerRef.camCon2 && other.CompareTag("Player")){
            activeCam.Priority = 10; // To make it work all the game cameras must be bellow this priority
            playerRef.camCon2 = true;
            Debug.Log(" Trigger: " + gameObject.name + " Câmera: " + activeCam.gameObject.name);
        }
    }


    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            activeCam.Priority = 0;
            playerRef.camCon2 = false;
        }
    } 
}