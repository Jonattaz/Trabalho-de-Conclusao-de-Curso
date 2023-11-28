using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ClockPuzzle : MonoBehaviour, IInteractable{
    
    private AudioSource audioSource;
    [SerializeField] private bool puzzleCompleted;
    [SerializeField] private bool hourHorario;
    [SerializeField] private bool hourAntihorario;
    [SerializeField] private bool minHorario;
    [SerializeField] private bool minAntihorario;
    [SerializeField] private PlayerMovement PlayerMovement;
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

        /* TEST - Fazer diferente, preciso ter a localização exata do ponteiro para saber se a hora está correta
            Em vez de mudar adicionar collider para quando o ponteiro estiver OntriggerStay nele isso irá 
            significar que o ponteiro está no lugar certo,
            caso os dois estiverem no lugar correto o puzzle estará correto
        */
        if(hourHorario)
            pointerHour.transform.Rotate(Vector3.right);

        if(hourAntihorario)
            pointerHour.transform.Rotate(-Vector3.right);

        if(minHorario)
            pointerMin.transform.Rotate(Vector3.right);

        if(minAntihorario)
            pointerMin.transform.Rotate(-Vector3.right);

    }

    public void Interact(){
              
    }

    public void Exit(){
        Time.timeScale = 1;
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
