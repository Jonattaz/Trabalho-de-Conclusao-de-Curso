using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestGoal{
    public GoalType GoalType;
    public int requiredAmount;
    public int currentAmount;

    public bool isReached(){
        return (currentAmount >= requiredAmount);
    } 
}

public enum GoalType{
    CollectItem,
    ItemDelivery
}