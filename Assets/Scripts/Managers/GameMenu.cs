using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour{

    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject tasksPanel;

    [SerializeField] public Text[] itemDescription;
    [SerializeField] public Text[] taskDescription;
    [SerializeField] public Text[] pageDescription; 
  
    [SerializeField] public Text[] itens;
    [SerializeField] public Text[] tasks;
    [SerializeField] public Text[] pages;

    [SerializeField] public GameObject[] itensButton;
    [SerializeField] public GameObject[] tasksButton;
    [SerializeField] public GameObject[] pagesButton;
   

    public static GameMenu instance;
    
    private void Awake(){
        instance = this;
    }

    // Update is called once per frame
    void Update(){        
        MenuController();

    }

    public void MenuController(){
        if(Input.GetKeyDown(menuKey)){
            menuPanel.active = !menuPanel.active;
        
            if(menuPanel.active)
                Time.timeScale = 0;
            else{
                Time.timeScale = 1 ; 
            }
        }
    }

    public void InventoryOn(){
        journalPanel.active = false;
        tasksPanel.active = false;
        inventoryPanel.active = !inventoryPanel.active;
    }

    public void JournalOn(){
        inventoryPanel.active = false;
        tasksPanel.active = false;
        journalPanel.active = !inventoryPanel.active;
    }

    public void TasksOn(){
        inventoryPanel.active = false;
        journalPanel.active = false;
        tasksPanel.active = !tasksPanel.active;
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

        for (int i = 0; i < pagesButton.Length; i++){
            if(buttonNumber == i){
                for(int j = 0; j < PlayerInventory.instance.pagesIndex; j++){
                    if(i == j){
                        pageDescription[j].gameObject.SetActive(true);
                    }
                }
            }else{
                pageDescription[i].gameObject.SetActive(false);
            }
        }

      

    }

    public void TaskDescription(){

    }

}






