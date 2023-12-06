using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DoorPuzzle : MonoBehaviour, IInteractable{

    private AudioSource audioSource;
    private bool interacting;    
    private int aux;
    [SerializeField] private bool startTimer; 
    [SerializeField] private float timer;
    [SerializeField] private float delayTime;
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
    [SerializeField] private Text obsUnlock;
    [SerializeField] private GameObject obsObject;
    [SerializeField] private GameObject unlockObject;
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private GameObject[] lockers;
    [SerializeField] private GameObject[] unlocker;

    // Pages
    [SerializeField] private PuzzleItem journalPage1;
    [SerializeField] private PuzzleItem journalPage2;


    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update(){
        if(startTimer)
            if (timer < delayTime){
                timer += Time.deltaTime;
            }else{
                timer = 0;
                StartCoroutine(CamFocus());   
                startTimer = false;
            }
    }
    public void ShowUnlock(){
        startTimer = true; 
    }

    IEnumerator CamFocus(){

        lockers[aux].SetActive(false);
        unlocker[aux].SetActive(true);
        audioSource.PlayOneShot(unlockedSound);
        PlayerMovement.movementConstraint = true;
        unlockObject.active = true;
        obsUnlock.text = unlockText;
        // Zoom in no objeto
        activeCam.Priority = 11;

        yield return new WaitForSeconds(camTime);

        PlayerMovement.movementConstraint = false;
        unlockObject.active = false;
        obsUnlock.text = unlockText;
        // Zoom in no objeto
        activeCam.Priority = 0;
        aux++;
    }


    public void Interact(){
        
        interacting = !interacting;
        
        if(interacting && !PlayerMovement.crouching){
            // Checks if the player has the pages in their inventory
            if(PlayerInventory.instance.items.Contains(journalPage1) || PlayerInventory.instance.items.Contains(journalPage2)){ 
        
                if(lessPages){
                    // Desbloquear porta
                    audioSource.enabled = false;
                    Debug.Log("Unlocked");
                    endGameUI.SetActive(true);
                    playerObj.SetActive(false);
                    enemyObj.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }else{
                    PlayerMovement.movementConstraint = true;
                    audioSource.PlayOneShot(lockedSound);
                    audioSource.enabled = true;
                    // Zoom in no objeto
                    activeCam.Priority = 11;
                    Cursor.visible = true;
                    obsObject.active = true;
                    obsText.text = doorText;
                }
                
            }else{        
                PlayerMovement.movementConstraint = true;
                audioSource.PlayOneShot(lockedSound);
                audioSource.enabled = true;
                // Zoom in no objeto
                activeCam.Priority = 11;
                Cursor.visible = true;
                obsObject.active = true;
                obsText.text = doorText;
            }

            if(PlayerInventory.instance.items.Contains(journalPage1) && PlayerInventory.instance.items.Contains(journalPage2)){
                // Desbloquear porta
                audioSource.enabled = false;
                Debug.Log("Unlocked");
                endGameUI.SetActive(true);
                playerObj.SetActive(false);
                enemyObj.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }             
        }else if(!interacting){ 
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
