using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{   
    [SerializeField] private float destroyTime;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision other){
        // Implement self destruction after a certain amount of time
        
        /*if(other.gameObject.tag == "Ground"){
            Destroy(this.gameObject, destroyTime);
        }*/

        Destroy(this.gameObject, destroyTime);
    }
}
