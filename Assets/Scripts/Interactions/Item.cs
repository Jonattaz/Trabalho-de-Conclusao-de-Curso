using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable{   
    
    [SerializeField] private float destroyTime;
    [SerializeField] private bool thrown;
    [SerializeField] ThrowItem throwItemRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
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
        // Implement self destruction after a certain amount of time
        
        if(thrown){
            if(other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy"){
                HearingManager.Instance.OnSoundEmitted(transform.position, EHeardSoundCategory.EItem, 0.2f);
                Destroy(this.gameObject, destroyTime);
            }
        }

    }
}
