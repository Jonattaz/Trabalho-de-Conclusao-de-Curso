using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour, IInteractable{

    [SerializeField] private GameObject ObsObject;
    [SerializeField] private Text ObsText;
    [SerializeField] private string Obs;
    [SerializeField] private PlayerMovement PlayerMovement;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void Interact(){
        
        ObsObject.active = !ObsObject.active;
        ObsText.text = Obs;
        PlayerMovement.movementConstraint = !PlayerMovement.movementConstraint;
    }
}
