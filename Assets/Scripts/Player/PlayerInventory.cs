using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour{

    public List<PuzzleItem> items;
 
    [HideInInspector]
    public GameObject itemPrefab;
    public static PlayerInventory instance;

    public Quest quest;

    public QuestGiver[] questGivers;

    public int index = 0;
    public int LastQuestIndex;
  
    [HideInInspector]
    public bool questObjective;

    public bool inQuest = false;
    
    public int itemIndex;
    public int pagesIndex;
    public int tasksIndex;

    private void Awake(){
        instance = this;
        
    }
    
    public void AddItem(PuzzleItem item){
        if (items.Contains(item))
        {   
            return;
        }
        //UI.instaceUI.SetItems(item, items.Count);
        items.Add(item);
        if(item.itemType == "Item"){
            if(GameMenu.instance.itens[itemIndex] != null){
                GameMenu.instance.itensButton[itemIndex].SetActive(true);
                GameMenu.instance.itens[itemIndex].text = item.itemName;
                GameMenu.instance.itemDescription[itemIndex].text = item.itemDescription;
                itemIndex++; 
            }


        }else if(item.itemType == "Task"){
            if(GameMenu.instance.tasks[tasksIndex] != null){
                GameMenu.instance.tasksButton[tasksIndex].SetActive(true);
                GameMenu.instance.tasks[tasksIndex].text = item.itemName;
                GameMenu.instance.taskDescription[tasksIndex].text = item.itemDescription;
                tasksIndex++;
            }

        }else if(item.itemType == "Page"){
            if(GameMenu.instance.pages[pagesIndex] != null){
                GameMenu.instance.pagesButton[pagesIndex].SetActive(true);
                GameMenu.instance.pages[pagesIndex].text = item.itemName;
                GameMenu.instance.pageDescription[pagesIndex].text = item.itemDescription;
                pagesIndex++;   
            }
        }else{
            Debug.Log("Algo está errado");
        }
        //QuestItem(item);
    }

    public void QuestItem(PuzzleItem item){

        if(questGivers[index].neededItemHolder == item.itemName){
            questGivers[index].completed = true;
            questObjective = true;
            //UI.instaceUI.UseItems(item, items.Count - 1);
        }
    }

    /*public void SpawnItems(int index){
        if(itemPrefab != null){
            Destroy(itemPrefab);
        }
       QuestItem(items[index]);
       itemPrefab = Instantiate(items[index].prefab, new Vector3(1000,1000,1000), Quaternion.identity);
       UI.instaceUI.Set3DCaptions(items[index].text);
        
    }*/
}

