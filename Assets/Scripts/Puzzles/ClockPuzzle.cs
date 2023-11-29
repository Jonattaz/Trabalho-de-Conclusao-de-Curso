using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ClockPuzzle : MonoBehaviour, IInteractable{
    
    private AudioSource audioSource;
    [SerializeField] private bool localPointerHourCorrect;
    [SerializeField] private bool localPointerMinCorrect;
    [SerializeField] private bool puzzleCompleted;
    [SerializeField] private float multiply;
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private GameObject AIObject;
    [SerializeField] private PointerClock[] pointers;
    [SerializeField] private Text puzzleUnlockText;
    [SerializeField] private Text obsText;
    [SerializeField] private string obsWithoutItem;
    [SerializeField] private string endPuzzleText;
    [SerializeField] private CinemachineVirtualCamera activeCam;
    [SerializeField] private CinemachineVirtualCamera pageCam;
    [SerializeField] private AudioClip audioClipEndPuzzle;
    [SerializeField] private PuzzleItem journalPage;
    [SerializeField] private GameObject obsObject;
    [SerializeField] private GameObject puzzleChoicesObject;
    [SerializeField] private GameObject otherButtons;
    [SerializeField] private GameObject pointerHour;
    [SerializeField] private GameObject pointerMin;
    [SerializeField] private GameObject vfxObj;
    [SerializeField] private GameObject[] timeMarkers;
    

    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){

    }

    public void ApplyChange(){
        
        for (int i = 0; i < pointers.Length; i++){ 
            
            if(pointers[i].whichPointer){

                if(pointers[i].pointerHour){
                    localPointerHourCorrect = true;
                }else{
                    localPointerHourCorrect = false;
                }

            }else{
                if(pointers[i].pointerMin){
                    localPointerMinCorrect = true;
                }else{
                    localPointerMinCorrect = false;
                }
            }
        }

        if(localPointerHourCorrect && localPointerMinCorrect){
            Debug.Log("Puzzle Correct");
        }else{
            Debug.Log("Puzzle Wrong");
        }
    }

    public void Interact(){

        for (int i = 0; i < pointers.Length; i++){
            pointers[i].gameObject.SetActive(true);
        }

        AIObject.SetActive(false);
        PlayerMovement.movementConstraint = true;
        vfxObj.SetActive(false);
        audioSource.enabled = true;
        // Zoom in no objeto
        activeCam.Priority = 11;
        puzzleChoicesObject.active = true;
        Cursor.visible = true;
        
    }

    public void HorasHorario(){
        pointerHour.transform.Rotate(Vector3.right * multiply);
    }

    public void HorasAntihorario(){
        pointerHour.transform.Rotate(-Vector3.right * multiply);

    }

    public void MinutosHorario(){
        pointerMin.transform.Rotate(Vector3.right * multiply);
    }


    public void MinutosAntihorario(){
        pointerMin.transform.Rotate(-Vector3.right * multiply);
    }


    public void Exit(){

        for (int i = 0; i < pointers.Length; i++){
            pointers[i].gameObject.SetActive(false);
        }


        AIObject.SetActive(true);
        // Desativar o som ao sair
        audioSource.enabled = false; 
        audioSource.Stop();

        // Zoom out 
        activeCam.Priority = 0;
        pageCam.Priority = 0;
        puzzleChoicesObject.active = false;
        PlayerMovement.movementConstraint = false;
        Cursor.visible = false;
        if(puzzleCompleted){
            audioSource.enabled = false;
            vfxObj.SetActive(false);
        }else{
            vfxObj.SetActive(true);
        }
    }        
}
