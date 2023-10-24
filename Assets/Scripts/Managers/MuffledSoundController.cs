using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuffledSoundController : MonoBehaviour{

    List<string> characters = new List<string>();
    // Low Pass Filter component reference
    [SerializeField] private AudioLowPassFilter lowPassFilter;

   
    void OnCollisionEnter(Collision col){
        
        // Add the tag of the object who collided with it to the list
        characters.Add(col.gameObject.tag);
        
        // Verify if characters Player and Enemy are in the list
        if(characters.Contains("Player") && characters.Contains("Enemy")){
           // Debug.Log("Not muffled");
            lowPassFilter.enabled = false;
        }else{
           // Debug.Log("Muffled");
            lowPassFilter.enabled = true;
        }

    }

    void OnCollisionExit(Collision col){
        // Remove the name from the list on collision exit
        characters.Remove(col.gameObject.name);
        
    }

}
