using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBoxPuzzle : MonoBehaviour, IInteractable{
    
    private AudioSource audioSource;
    [SerializeField] private PuzzleItem questItem;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject ObsObject;
    [SerializeField] private Text ObsText;
    [SerializeField] private string ObsWithoutItem;
    [SerializeField] private string ObsWithItem;
    [SerializeField] private PlayerMovement PlayerMovement;


    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
       
    }


    public void Interact(){
        // Checks if the player has the music box piece in their inventory
        if(PlayerInventory.instance.items.Contains(questItem)){            
            //Debug.Log("Possui a peça");
            
            if(audioClip != null)
                audioSource.PlayOneShot(audioClip);

            ObsObject.active = !ObsObject.active;
            ObsText.text = ObsWithItem;
            PlayerMovement.movementConstraint = !PlayerMovement.movementConstraint;
            Cursor.visible = !Cursor.visible;

        }else{
            //Debug.Log("Não possui a peça");
            ObsObject.active = !ObsObject.active;
            ObsText.text = ObsWithoutItem;
            PlayerMovement.movementConstraint = !PlayerMovement.movementConstraint;
        }        
    }
}

