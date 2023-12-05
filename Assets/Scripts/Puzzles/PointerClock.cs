using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerClock : MonoBehaviour{

    public bool whichPointer;   // True - Hora
                                // False - Minutos
    public bool pointerHour;
    public bool pointerMin;
    public bool puzzleStarted;
        
    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
    void OnTriggerStay(Collider other){

        if(puzzleStarted){
            if(whichPointer){
                if(other.gameObject.tag == gameObject.name){
                    pointerHour = true;
                }else{
                    pointerHour = false;
                }
            }else{
                if(other.gameObject.tag == gameObject.name){
                    pointerMin = true;
                }else{
                    pointerMin = false;
                }
            }
        }
    }
}









