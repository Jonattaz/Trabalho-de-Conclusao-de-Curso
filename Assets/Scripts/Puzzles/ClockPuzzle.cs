using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ClockPuzzle : MonoBehaviour, IInteractable{
    
    private AudioSource audioSource;
    [SerializeField] private bool canPuzzle;
    [SerializeField] private bool localPointerHourCorrect;
    [SerializeField] private bool localPointerMinCorrect;
    [SerializeField] private bool puzzleCompleted;
    [SerializeField] private float multiplyHours;
    [SerializeField] private float multiplyMinutes;
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
    [SerializeField] private GameObject puzzleChoicesObject;
    [SerializeField] private GameObject pointerHour;
    [SerializeField] private GameObject pointerMin;    
    [SerializeField] private GameObject otherButtons;
    

    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
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

            pageCam.Priority = 11;
            puzzleUnlockText.text = endPuzzleText;
            puzzleCompleted = true;
            otherButtons.SetActive(false);
            journalPage.gameObject.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            
            if(audioClipEndPuzzle != null)
                audioSource.PlayOneShot(audioClipEndPuzzle);

        }else{
            Debug.Log("Puzzle Wrong");
        }
    }

    public void Interact(){

        if(!puzzleCompleted){

            for (int i = 0; i < pointers.Length; i++){
                pointers[i].puzzleStarted = true;
            }

            AIObject.GetComponent<EnemyFSM>().stopAI = true;
            PlayerMovement.movementConstraint = true;
            audioSource.enabled = true;
            // Zoom in no objeto
            activeCam.Priority = 11;
            puzzleChoicesObject.SetActive(true);
            Cursor.visible = true;
        }
        
    }

    public void HorasHorario(){
        pointerHour.transform.Rotate(Vector3.right * multiplyHours);
    }

    public void HorasAntihorario(){
        pointerHour.transform.Rotate(-Vector3.right * multiplyHours);

    }

    public void MinutosHorario(){
        pointerMin.transform.Rotate(Vector3.right * multiplyMinutes);
    }


    public void MinutosAntihorario(){
        pointerMin.transform.Rotate(-Vector3.right * multiplyMinutes);
    }


    public void Exit(){

        for (int i = 0; i < pointers.Length; i++){
            pointers[i].puzzleStarted = false;
        }

        // Desativar o som ao sair
        audioSource.enabled = false; 
        audioSource.Stop();

        // Zoom out 
        activeCam.Priority = 0;
        pageCam.Priority = 0;
        puzzleChoicesObject.SetActive(false);
        PlayerMovement.movementConstraint = false;
        Cursor.visible = false;
        AIObject.GetComponent<EnemyFSM>().stopAI = false;

        if(puzzleCompleted){
            audioSource.enabled = false;
        }
    }        
}
