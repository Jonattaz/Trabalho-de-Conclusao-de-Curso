using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private CinemachineVirtualCamera activeCam;

    /// OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            activeCam.Priority = 10; // To make it work all the game cameras must be bellow this priority
        }
    }

    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            activeCam.Priority = 0;
        }        
    }
}
