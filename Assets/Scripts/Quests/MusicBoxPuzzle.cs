using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBoxPuzzle : MonoBehaviour, IInteractable{

    //General    
    private AudioSource audioSource;
    private Renderer renderer;
    private bool verify;
    [SerializeField] private Material[] materials;
    [SerializeField] private AudioClip audioClipInteraction;
    [SerializeField] private AudioClip audioClipUnlock;
    [SerializeField] private PuzzleItem musicBoxPiece;
    [SerializeField] private PuzzleItem journalPage;
    [SerializeField] private PuzzleItem obituaryPage;
    [SerializeField] private GameObject obsObject;
    [SerializeField] private GameObject puzzleChoicesObject;
    [SerializeField] private Text puzzleUnlockText;
    [SerializeField] private Text obsText;
    [SerializeField] private string obsWithoutItem;
    [SerializeField] private string endPuzzleText;
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private bool puzzleCompleted;

    // Pins
    [SerializeField] private bool IPin;
    [SerializeField] private bool IIPin;
    [SerializeField] private bool IIIPin;
    [SerializeField] private bool IVPin;

    // Pins Audio
    [SerializeField] private AudioClip IASound;
    [SerializeField] private AudioClip IBSound;
    [SerializeField] private AudioClip IIASound;
    [SerializeField] private AudioClip IIBSound;
    [SerializeField] private AudioClip IIIASound;
    [SerializeField] private AudioClip IVASound;
    [SerializeField] private AudioClip IVBSound;

    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
        renderer = GetComponent<MeshRenderer>();
        renderer.material = materials[0];  
    }

    // Update is called once per frame
    void Update(){
       if(verify){
            if(PlayerInventory.instance.items.Contains(journalPage) && PlayerInventory.instance.items.Contains(obituaryPage)){
                renderer.material = materials[0];  
                gameObject.GetComponent<MusicBoxPuzzle>().enabled = false; 
            }
       }
    }

    public void Interact(){
        
        if(!puzzleCompleted){
            // Checks if the player has the music box piece in their inventory
            if(PlayerInventory.instance.items.Contains(musicBoxPiece)){            
                //Debug.Log("Possui a peça");
                
                if(audioClipInteraction != null)
                    audioSource.PlayOneShot(audioClipInteraction);

                // Zoom no objeto
                puzzleChoicesObject.active = true;
                PlayerMovement.movementConstraint = true;
                Cursor.visible = true;

            }else{
                //Debug.Log("Não possui a peça");
                obsObject.active = !obsObject.active;
                obsText.text = obsWithoutItem;
                PlayerMovement.movementConstraint = !PlayerMovement.movementConstraint;
            }  
        }      
    }

    /* Pin = true = A
        Pin = false = B
    */
    public void ChoiceI(){
        IPin = !IPin;
        if(IPin){
            //audioSource.PlayOneShot(IASound);
            Debug.Log("IASound playing...");
        }else{
            //audioSource.PlayOneShot(IBSound);
            Debug.Log("IBSound playing...");
        }
    }

    public void ChoiceII(){
        IIPin = !IIPin;
        if(IIPin){
            //audioSource.PlayOneShot(IIASound);
            Debug.Log("IIASound playing...");
        }else{
            //audioSource.PlayOneShot(IIBSound);
            Debug.Log("IIBSound playing...");
        }

    }

    public void ChoiceIII(){
        IIIPin = !IIIPin;
        if(IIIPin){
            //audioSource.PlayOneShot(IIIASound);
            Debug.Log("IIIASound playing...");
        }else{
            //audioSource.PlayOneShot(IIIBSound);
            Debug.Log("IIIBSound playing...");
        }
    }

    public void ChoiceIV(){
        IVPin = !IVPin;
        if(IVPin){
            //audioSource.PlayOneShot(IVASound);
            Debug.Log("IVASound playing...");
        }else{
            //audioSource.PlayOneShot(IVBSound);
            Debug.Log("IVBSound playing...");
        }
    }

    public void Play(){

         if(IPin && !IIPin && !IIIPin && IVPin){

            // Animação da bailarina dançando e a música tocando
            // Ao terminar tocar o barulho de algo destrancando
            //audioSource.PlayOneShot(audioClipUnlock);
            puzzleUnlockText.text = endPuzzleText;
            puzzleCompleted = true;
            renderer.material = materials[1];
            obituaryPage.gameObject.SetActive(true);
            journalPage.gameObject.SetActive(true);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            verify = true;

         }else{

            // Apenas a animação da bailarina rotacionando e musica desafinada tocando
            if(!IPin && !IIPin && !IIIPin && !IVPin){

            }else if(IPin && IIPin && IIIPin && IVPin){

            }else if(IPin && !IIPin && !IIIPin && !IVPin){

            }else if(!IPin && IIPin && !IIIPin && !IVPin){

            }else if(!IPin && !IIPin && IIIPin && !IVPin){

            }else if(!IPin && !IIPin && !IIIPin && IVPin){

            }else if(IPin && IIPin && !IIIPin && !IVPin){

            }else if(IPin && !IIPin && IIIPin && !IVPin){

            }else if(!IPin && IIPin && IIIPin && !IVPin){

            }else if(!IPin && IIPin && !IIIPin && IVPin){

            }else if(!IPin && !IIPin && IIIPin && IVPin){

            }else if(!IPin && IIPin && IIIPin && IVPin){

            }else if(IPin && !IIPin && IIIPin && IVPin){

            }else if(IPin && IIPin && !IIIPin && IVPin){

            }else if(IPin && IIPin && IIIPin && !IVPin){

            }
         }
    }

    public void Exit(){

        puzzleChoicesObject.active = false;
        PlayerMovement.movementConstraint = false;
        Cursor.visible = false;

    }



}

