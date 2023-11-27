using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DoorPuzzle : MonoBehaviour, IInteractable{

    private AudioSource audioSource;
    private bool interacting;
    [SerializeField] private AudioClip lockedSound;
    [SerializeField] private AudioClip unlockedSound;
    [SerializeField] private bool lessPages;
    [SerializeField] private bool camShowLock;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private float camTime;
    [SerializeField] private float maxCamTime;
    [SerializeField] private CinemachineVirtualCamera activeCam;
    [SerializeField] private string doorText;
    [SerializeField] private string unlockText;
    [SerializeField] private Text obsText;
    [SerializeField] private GameObject obsObject;
    [SerializeField] private PlayerMovement PlayerMovement;


    // Pages
    [SerializeField] private PuzzleItem journalPage;
    [SerializeField] private PuzzleItem obituaryPage;



    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void ShowUnlock(){
        StartCoroutine(CamFocus());
    }

    IEnumerator CamFocus(){

        audioSource.PlayOneShot(unlockedSound);
        PlayerMovement.movementConstraint = true;
        obsObject.active = true;
        obsText.text = unlockText;
        // Zoom in no objeto
        activeCam.Priority = 11;

        yield return new WaitForSeconds(camTime);

        PlayerMovement.movementConstraint = false;
        obsObject.active = false;
        obsText.text = unlockText;
        // Zoom in no objeto
        activeCam.Priority = 0;
    }


    public void Interact(){
        
        interacting = !interacting;
        
        if(interacting && !PlayerMovement.crouching){
            // Checks if the player has the pages in their inventory
            if(PlayerInventory.instance.items.Contains(journalPage)){ 
                
                if(lessPages){
                    // Desbloquear porta
                    audioSource.enabled = false;
                    Debug.Log("Unlocked");
                    endGameUI.SetActive(true);
                    playerObj.SetActive(false);
                    enemyObj.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                /*else if(PlayerInventory.instance.items.Contains(SecondPage) && PlayerInventory.instance.items.Contains(thirdPage)){
                    // Desbloquear porta
                    audioSource.enabled = false;
                }*/

            }else{        
                Time.timeScale = 0;
                PlayerMovement.movementConstraint = true;
                audioSource.PlayOneShot(lockedSound);
                audioSource.enabled = true;
                // Zoom in no objeto
                activeCam.Priority = 11;
                Cursor.visible = true;
                obsObject.active = true;
                obsText.text = doorText;
            }             
        }else if(!interacting){ 
            Time.timeScale = 1;
            // Desativar o som ao sair
            audioSource.enabled = false; 
            audioSource.Stop();

            // Zoom out 
            activeCam.Priority = 0;
            PlayerMovement.movementConstraint = false;
            Cursor.visible = false;
            obsObject.active = false;
        }
    }      
}
