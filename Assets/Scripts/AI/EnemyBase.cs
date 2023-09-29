using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour{
   
    public virtual void OnTargetDetected(GameObject target){
        Debug.Log("[" + gameObject.name + "] Detected " + target.name);
    }

    public virtual void OnTargetLost(GameObject target){
        Debug.Log("[" + gameObject.name + "] Lost " + target.name);
    }
    
    public virtual void OnSoundHeard(Vector3 location){
        Debug.Log("[" + gameObject.name + "] heard something at " + location);
    }
    
}
