using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerQuests playerQuests;

    public QuestGiver instance;
    public GameObject questWindow;
    public Text title;
    public Text description;
    public Text neededItem;
    public bool completed = false;

    [HideInInspector]
    public bool localActive;

    [SerializeField]
    private Item questItem;

    [HideInInspector]
    public string neededItemHolder;
    public int questIndex;
    
    private void Awake() {
        instance = this;
    }

    public void OpenQuestWindow(){
        questWindow.SetActive(true);
        title.text = quest.title;
        description.text = quest.description;
        neededItem.text = "Item necessário - " + quest.neededItem;
        neededItemHolder = quest.neededItem;
        quest.isActive = true;
        localActive = quest.isActive;
        playerQuests.quests[questIndex] = quest;

         StartCoroutine(ItemQuestVerifier());
    }


    IEnumerator ItemQuestVerifier(){
       completed = true;
       yield return new WaitForSeconds(3);
       if(PlayerInventory.instance.items.Contains(questItem)){
            CompleteQuest();
            PlayerInventory.instance.inQuest = false;
        }
    }

    public void CompleteQuest(){
        
        PlayerInventory.instance.questObjective = false;
        questWindow.SetActive(false);
        quest.isActive = false;
        title.text = "";
        description.text = "";
        neededItem.text = "";
        localActive = quest.isActive;
        PlayerInventory.instance.index++;
        Destroy(this.gameObject);
    }
}
