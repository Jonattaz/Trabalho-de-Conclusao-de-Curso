using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableTarget : MonoBehaviour{



    // Start is called before the first frame update
    void Start(){
        DetectableTargetManager.Instance.Register(this);    
    }

    // Update is called once per frame
    void Update(){
        
    }

    // This function is called when the MonoBehaviour will be destroyed.
    void OnDestroy(){
        if(DetectableTargetManager.Instance != null){
            DetectableTargetManager.Instance.Deregister(this);
        }
    }
}
