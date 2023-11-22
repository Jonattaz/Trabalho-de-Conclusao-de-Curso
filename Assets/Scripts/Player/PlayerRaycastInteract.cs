using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastInteract : MonoBehaviour{
    
    private PlayerMovement playerMovement;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;                
    [SerializeField] private float raycastRange;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform orientationPoint;

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update(){
        InteractionController();
    }


    private void InteractionController(){
        RaycastHit hit;

        if(Physics.Raycast(orientationPoint.position, orientationPoint.forward, out hit, raycastRange, layerMask)){
            
            //Debug.Log("O raycast está acertando " + hit.collider.gameObject.name);

            var hitObjectInteract = hit.collider.GetComponent<IInteractable>();
            if(hitObjectInteract != null){
                if(Input.GetKeyDown(interactionKey)){
                    hitObjectInteract.Interact();
                }
            } 

        }
    }
}
