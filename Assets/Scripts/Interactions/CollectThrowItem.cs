using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectThrowItem : MonoBehaviour, IInteractable{   
    
    [SerializeField] private float destroyTime;
    [SerializeField] private bool thrown;
    [SerializeField] ThrowItem throwItemRef;
      
    public void CollectItem(){

        gameObject.SetActive(false);
        throwItemRef.totalThrows++;
        throwItemRef.throwsText.text = throwItemRef.totalThrows.ToString();
        thrown = true;
        Destroy(this.gameObject);
    }

    public void Interact(){
        if(!thrown)
            CollectItem();
    }


    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision other){
        
        if(thrown){
            if(other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy"){
                HearingManager.Instance.OnSoundEmitted(gameObject, transform.position, EHeardSoundCategory.EItem, 2f);
                Destroy(this.gameObject, destroyTime);
            }
        }

    }
}
