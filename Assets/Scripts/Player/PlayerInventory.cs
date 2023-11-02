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

    private void Awake(){
        instance = this;
        
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update(){
        
    }
    
    public void AddItem(PuzzleItem item){
        if (items.Contains(item))
        {   
            return;
        }
        //UI.instaceUI.SetItems(item, items.Count);
        items.Add(item);
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

