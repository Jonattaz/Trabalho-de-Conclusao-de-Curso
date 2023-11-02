using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MusicBoxPuzzle : MonoBehaviour, IInteractable{

    //General    
    private AudioSource audioSource;
    private Renderer renderer;
    private bool verify;
    [SerializeField] private Material[] materials;
    [SerializeField] private CinemachineVirtualCamera activeCam;
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

    /* Pins Audio
        Ordem
        pinSound[0] = IA
        pinSound[1] = IB
        pinSound[2] = IIA
        pinSound[3] = IIB
        pinSound[4] = IIIA
        pinSound[5] = IIIB
        pinSound[6] = IVA
        pinSound[7] = IVB
    */
    public AudioClip[] pinSound;
    public AudioClip[] pinsSound;

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
                
                if(audioClipInteraction != null)
                    audioSource.PlayOneShot(audioClipInteraction);

                // Zoom in no objeto
                activeCam.Priority = 11;
                puzzleChoicesObject.active = true;
                PlayerMovement.movementConstraint = true;
                Cursor.visible = true;

            }else{
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
            audioSource.PlayOneShot(pinSound[0]);
            Debug.Log("IASound playing...");
        }else{
            audioSource.PlayOneShot(pinSound[1]);
            Debug.Log("IBSound playing...");
        }
    }

    public void ChoiceII(){
        IIPin = !IIPin;
        if(IIPin){
            audioSource.PlayOneShot(pinSound[2]);
            Debug.Log("IIASound playing...");
        }else{
            audioSource.PlayOneShot(pinSound[3]);
            Debug.Log("IIBSound playing...");
        }

    }

    public void ChoiceIII(){
        IIIPin = !IIIPin;
        if(IIIPin){
            audioSource.PlayOneShot(pinSound[4]);
            Debug.Log("IIIASound playing...");
        }else{
            audioSource.PlayOneShot(pinSound[5]);
            Debug.Log("IIIBSound playing...");
        }
    }

    public void ChoiceIV(){
        IVPin = !IVPin;
        if(IVPin){
            audioSource.PlayOneShot(pinSound[6]);
            Debug.Log("IVASound playing...");
        }else{
            audioSource.PlayOneShot(pinSound[7]);
            Debug.Log("IVBSound playing...");
        }
    }

    public void Play(){

         if(IPin && !IIPin && !IIIPin && IVPin){

            // Animação da bailarina dançando e a música tocando
            // Ao terminar tocar o barulho de algo destrancando
            StartCoroutine(playAudioSequentially(pinSound[0], pinSound[3], pinSound[5], pinSound[6]));
            puzzleUnlockText.text = endPuzzleText;
            puzzleCompleted = true;
            renderer.material = materials[1];
            obituaryPage.gameObject.SetActive(true);
            journalPage.gameObject.SetActive(true);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            verify = true;
            audioSource.PlayOneShot(audioClipUnlock);
            
         }else{
            // Apenas a animação da bailarina rotacionando e musica desafinada tocando
            if(!IPin && !IIPin && !IIIPin && !IVPin){
                StartCoroutine(playAudioSequentially(pinSound[1], pinSound[3], pinSound[5], pinSound[7]));
            }else if(IPin && IIPin && IIIPin && IVPin){
                StartCoroutine(playAudioSequentially(pinSound[0], pinSound[2], pinSound[5], pinSound[6]));
            }else if(IPin && !IIPin && !IIIPin && !IVPin){
                StartCoroutine(playAudioSequentially(pinSound[0], pinSound[3], pinSound[5], pinSound[7]));
            }else if(!IPin && IIPin && !IIIPin && !IVPin){
                StartCoroutine(playAudioSequentially(pinSound[1], pinSound[2], pinSound[5], pinSound[7]));
            }else if(!IPin && !IIPin && IIIPin && !IVPin){
                StartCoroutine(playAudioSequentially(pinSound[1], pinSound[3], pinSound[4], pinSound[7]));
            }else if(!IPin && !IIPin && !IIIPin && IVPin){
                StartCoroutine(playAudioSequentially(pinSound[1], pinSound[3], pinSound[5], pinSound[6]));
            }else if(IPin && IIPin && !IIIPin && !IVPin){
                StartCoroutine(playAudioSequentially(pinSound[0], pinSound[2], pinSound[5], pinSound[7]));
            }else if(IPin && !IIPin && IIIPin && !IVPin){
                StartCoroutine(playAudioSequentially(pinSound[0], pinSound[3], pinSound[4], pinSound[7]));
            }else if(!IPin && IIPin && IIIPin && !IVPin){
                StartCoroutine(playAudioSequentially(pinSound[1], pinSound[2], pinSound[4], pinSound[7]));
            }else if(!IPin && IIPin && !IIIPin && IVPin){
                StartCoroutine(playAudioSequentially(pinSound[1], pinSound[2], pinSound[5], pinSound[6]));
            }else if(!IPin && !IIPin && IIIPin && IVPin){
                StartCoroutine(playAudioSequentially(pinSound[1], pinSound[3], pinSound[4], pinSound[6]));
            }else if(!IPin && IIPin && IIIPin && IVPin){
                StartCoroutine(playAudioSequentially(pinSound[1], pinSound[2], pinSound[4], pinSound[6]));
            }else if(IPin && !IIPin && IIIPin && IVPin){
                StartCoroutine(playAudioSequentially(pinSound[0], pinSound[3], pinSound[4], pinSound[6]));
            }else if(IPin && IIPin && !IIIPin && IVPin){
                StartCoroutine(playAudioSequentially(pinSound[0], pinSound[2], pinSound[5], pinSound[6]));
            }else if(IPin && IIPin && IIIPin && !IVPin){
                StartCoroutine(playAudioSequentially(pinSound[0], pinSound[2], pinSound[4], pinSound[7]));
            }
         }
    }

    public void Exit(){
        // Zoom out 
        activeCam.Priority = 0;
        puzzleChoicesObject.active = false;
        PlayerMovement.movementConstraint = false;
        Cursor.visible = false;
        if(puzzleCompleted){
            audioSource.enabled = false;
        }
    }

    IEnumerator playAudioSequentially(AudioClip IPin, AudioClip IIPin, AudioClip IIIPin, AudioClip IVPin){
        
        yield return null;

        pinsSound[0] = IPin;
        pinsSound[1] = IIPin;
        pinsSound[2] = IIIPin;
        pinsSound[3] = IVPin;

        //1.Loop through each AudioClip
        for (int i = 0; i < pinsSound.Length; i++){
                
            //2.Assign current AudioClip to audiosource
            audioSource.clip = pinsSound[i];

            //3.Play Audio
            audioSource.Play();

            //4.Wait for it to finish playing
            while (audioSource.isPlaying){
                yield return null;
            }

                //5. Go back to #2 and play the next audio in the adClips array
        }
    }

}

