using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffledSoundController : MonoBehaviour{

    List<string> contacts = new List<string>();
    [SerializeField] private AudioLowPassFilter lowPassFilter;

   
    void OnCollisionEnter(Collision col){
        
        contacts.Add(col.gameObject.tag);
        
        if(contacts.Contains("Player") && contacts.Contains("Enemy")){
            Debug.Log("Not muffled");
            lowPassFilter.enabled = false;
        }else{
            Debug.Log("Muffled");
            lowPassFilter.enabled = true;
        }

    }

    void OnCollisionExit(Collision col){
        contacts.Remove(col.gameObject.name);
        
    }

}
