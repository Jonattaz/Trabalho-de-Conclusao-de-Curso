using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastInteract : MonoBehaviour{
    
    [SerializeField] private float raycastRange = 5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform orientationPoint;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;


    // Update is called once per frame
    void Update(){

        InteractionController();
        
    }


    private void InteractionController(){
        RaycastHit hit;

        if(Physics.Raycast(orientationPoint.position, orientationPoint.forward, out hit, raycastRange, layerMask)){
            
            Debug.Log("O raycast está acertando " + hit.collider.gameObject.name);

            var hitObjectInteract = hit.collider.GetComponent<IInteractable>();
            if(hitObjectInteract != null){
                if(Input.GetKeyDown(interactionKey)){
                    hitObjectInteract.Interact();
                }
            } 

        }
    }
}
