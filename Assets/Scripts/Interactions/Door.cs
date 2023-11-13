using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable{

    [SerializeField] private PuzzleItem key;
    [SerializeField] private AudioClip lockedSound;
    [SerializeField] private AudioClip unlockedSound;
    [SerializeField] private GameObject ObsObject;
    [SerializeField] private Text ObsText;
    [SerializeField] private string Obs;
    [SerializeField] private PlayerMovement PlayerMovement;
    private AudioSource audioSource;
    
    private bool opened;
    public bool locked;

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact(){

        if(PlayerInventory.instance.items.Contains(key))
            locked = false;

        if(locked){
            if(lockedSound != null)
                audioSource.PlayOneShot(lockedSound);
            ObsObject.active = !ObsObject.active;
            ObsText.text = Obs;
            PlayerMovement.movementConstraint = !PlayerMovement.movementConstraint;
        }else{

            if(!opened){
                opened = true;
                // Play open animation
                if(unlockedSound != null)
                    audioSource.PlayOneShot(unlockedSound);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<Renderer> ().enabled = false;
            }     
        }
        
    }
}
