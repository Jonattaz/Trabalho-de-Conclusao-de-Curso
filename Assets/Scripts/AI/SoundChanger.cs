using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour{

    [SerializeField] private float TimeChanger;
    [SerializeField] private float RemainingTime;
    [SerializeField] private int index;
    [SerializeField] private GameObject[] AudioClips;
   
    // Start is called before the first frame update
    void Start(){
        
        RemainingTime = TimeChanger;

        index = 0;  
    }

    // Update is called once per frame
    void Update(){
        
        ChangeSound();

    }

    private void ChangeSound(){
        
        if(RemainingTime <= 0){
            AudioClips[index].SetActive(false);
            
            RemainingTime = TimeChanger;
            index++;

            if(index >= AudioClips.Length){
                index = 0;
            }

            AudioClips[index].SetActive(true);
    
        }else{
            RemainingTime -= Time.deltaTime;
        }
    }

}
