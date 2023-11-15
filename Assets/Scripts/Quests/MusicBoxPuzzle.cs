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
    private bool canPuzzle;
    [SerializeField] private bool canUnlock;
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

    /* Pins Object
        Ordem
        pinObject[0] = IA
        pinObject[1] = IB
        pinObject[2] = IIA
        pinObject[3] = IIB
        pinObject[4] = IIIA
        pinObject[5] = IIIB
        pinObject[6] = IVA
        pinObject[7] = IVB
    */

    public GameObject[] pinObject;

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
            audioSource.enabled = true;
            // Checks if the player has the music box piece in their inventory
            if(PlayerInventory.instance.items.Contains(musicBoxPiece) || canPuzzle ){            
                PlayerInventory.instance.items.Remove(musicBoxPiece);
                for (int i = 0; i < GameMenu.instance.itens.Length; i++){
                    
                    if(GameMenu.instance.itens[i].text == musicBoxPiece.itemName){
                        GameMenu.instance.itensButton[i].SetActive(false);
                        GameMenu.instance.itens[i].text = ""; 
                        GameMenu.instance.itemDescription[i].gameObject.SetActive(false);
                    }

                }

                if(!canPuzzle)
                    audioSource.PlayOneShot(audioClipInteraction);
    
                // Zoom in no objeto
                activeCam.Priority = 11;
                puzzleChoicesObject.active = true;
                PlayerMovement.movementConstraint = true;
                Cursor.visible = true;
                canPuzzle = true;
                
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
        
        if(!audioSource.isPlaying){
            IPin = !IPin;
            if(IPin){
                pinObject[0].SetActive(false);
                pinObject[1].SetActive(true);
                audioSource.PlayOneShot(pinSound[0]);
                Debug.Log("IASound playing...");
            }else{
                pinObject[1].SetActive(false);
                pinObject[0].SetActive(true);
                audioSource.PlayOneShot(pinSound[1]);
                Debug.Log("IBSound playing...");
            }
        }else{
            Debug.Log("Não tocou");
        }
    }

    public void ChoiceII(){
        if(!audioSource.isPlaying){
            IIPin = !IIPin;
            if(IIPin){
                pinObject[3].SetActive(false);
                pinObject[2].SetActive(true);
                audioSource.PlayOneShot(pinSound[2]);
                Debug.Log("IIASound playing...");
            }else{
                pinObject[2].SetActive(false);
                pinObject[3].SetActive(true);
                audioSource.PlayOneShot(pinSound[3]);
                Debug.Log("IIBSound playing...");
            }
        }
    }

    public void ChoiceIII(){
        
        if(!audioSource.isPlaying){
            IIIPin = !IIIPin;
            if(IIIPin){
                pinObject[5].SetActive(false);
                pinObject[4].SetActive(true);
                audioSource.PlayOneShot(pinSound[4]);
                Debug.Log("IIIASound playing...");
            }else{
                pinObject[4].SetActive(false);
                pinObject[5].SetActive(true);
                audioSource.PlayOneShot(pinSound[5]);
                Debug.Log("IIIBSound playing...");
            }
        }
    }

    public void ChoiceIV(){
        if(!audioSource.isPlaying){
            IVPin = !IVPin;
            if(IVPin){
                pinObject[7].SetActive(false);
                pinObject[6].SetActive(true);
                audioSource.PlayOneShot(pinSound[6]);
                Debug.Log("IVASound playing...");
            }else{
                pinObject[6].SetActive(false);
                pinObject[7].SetActive(true);
                audioSource.PlayOneShot(pinSound[7]);
                Debug.Log("IVBSound playing...");
            }
        }
    }

    public void Play(){
        if(!audioSource.isPlaying){
            if(IPin && !IIPin && !IIIPin && IVPin){

                // Animação da bailarina dançando e a música tocando
                // Ao terminar tocar o barulho de algo destrancando
                StartCoroutine(playAudioSequentially(pinSound[0], pinSound[3], pinSound[5], pinSound[6]));
                canUnlock = true;
                
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
                }else if(IPin && IIPin  && !IIIPin && !IVPin){
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
    }

    public void Exit(){
        // Desativar o som ao sair
        audioSource.enabled = false; 
        audioSource.Stop();

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
            
            if(i == 3 && canUnlock){
                puzzleUnlockText.text = endPuzzleText;
                puzzleCompleted = true;
                renderer.material = materials[1];
                obituaryPage.gameObject.SetActive(true);
                journalPage.gameObject.SetActive(true);
                gameObject.GetComponent<SphereCollider>().enabled = false;
                verify = true;
                audioSource.PlayOneShot(audioClipUnlock);
            }
        }
    }
}

