using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour{
    
    int var;
    int auxBackwards = 1;
    int auxForwards = 1;
    private PlayerMovement playerMovement;
    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject pagesPanel; 
    
    [SerializeField] private int pageDescriptionNumber;

    public Text[] itemDescription;
    public Text[] taskDescription;
    public Text[] pageDescription; 
  
    public Text[] itens;
    public Text[] tasks;
    public Text[] pages;

    public GameObject[] itensButton;
    public GameObject[] tasksButton;
    public GameObject[] pagesButton;
    public GameObject[] imagesPanel;
    public GameObject[] pagesImages;
   
    public static GameMenu instance;
    
    private void Awake(){
        instance = this;
    }

    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    void Start(){
        playerMovement = GetComponent<PlayerMovement>();        
    }

    // Update is called once per frame
    void Update(){        
        if(!playerMovement.movementConstraint)
            MenuController();
    }

    public void MenuController(){
        if(Input.GetKeyDown(menuKey)){
            menuPanel.active = !menuPanel.active;
        
            if(menuPanel.active)
                Time.timeScale = 0;
            else{
                Time.timeScale = 1; 
            }
        }
    }

    public void InventoryOn(){
        journalPanel.active = false;
        pagesPanel.active = false;
        inventoryPanel.active = !inventoryPanel.active;
    }

    public void JournalOn(){
        inventoryPanel.active = false;
        pagesPanel.active = false;
        journalPanel.active = !inventoryPanel.active;
    }

    public void MenuOn(){
        journalPanel.active = false;
        inventoryPanel.active = false;
        menuPanel.active = true;
    }

    public void PageOff(){
        pagesPanel.active = false;
    }


    // Funciona apenas uma vez - Arrumar
    public void Backwards(){
        var = pageDescriptionNumber - auxBackwards; 
        for (int i = 0; i < pagesButton.Length; i++){
            if(var == i){
                pageDescription[i+1].gameObject.SetActive(false);
                imagesPanel[i+1].gameObject.SetActive(false);
                pagesImages[i+1].gameObject.SetActive(false);
                
                pageDescription[i].gameObject.SetActive(true);
                imagesPanel[i].gameObject.SetActive(true);
                pagesImages[i].gameObject.SetActive(true);
                auxBackwards++;
                auxForwards--;
            }
        }

    }

    // Funciona apenas uma vez - Arrumar
    public void Forwards(){
        var = pageDescriptionNumber + auxForwards; 
        for (int i = 0; i < pagesButton.Length; i++){
            if(var == i){
                pageDescription[i-1].gameObject.SetActive(false);
                imagesPanel[i-1].gameObject.SetActive(false);
                pagesImages[i-1].gameObject.SetActive(false);
                
                pageDescription[i].gameObject.SetActive(true);
                imagesPanel[i].gameObject.SetActive(true);
                pagesImages[i].gameObject.SetActive(true);
                auxForwards--;
                auxBackwards++;
            }
        } 
    }

    public void ItemDescriptionButton(int buttonNumber){
    
        for (int i = 0; i < itensButton.Length; i++){
            if(buttonNumber == i){
                for(int j = 0; j < PlayerInventory.instance.itemIndex; j++){
                    if(i == j){
                        itemDescription[j].gameObject.SetActive(true);
                    }
                }
            }else{
                itemDescription[i].gameObject.SetActive(false);
            }
        }
    }

    public void PageDescriptionButton(int buttonNumber){

        pageDescriptionNumber = buttonNumber;
        auxForwards = 1;
        auxBackwards = 1;
        
        for (int i = 0; i < pagesButton.Length; i++){
            if(buttonNumber == i){
                for(int j = 0; j < PlayerInventory.instance.pagesIndex; j++){
                    if(i == j){
                        pageDescription[j].gameObject.SetActive(true);
                        imagesPanel[j].gameObject.SetActive(true);
                        pagesImages[j].gameObject.SetActive(true);
                        pagesPanel.active = true;
                    }
                }
            }else{
                pageDescription[i].gameObject.SetActive(false);
                imagesPanel[i].gameObject.SetActive(false);
                pagesImages[i].gameObject.SetActive(false);
            }
        }

    }

    public void CloseMenu(){
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}






