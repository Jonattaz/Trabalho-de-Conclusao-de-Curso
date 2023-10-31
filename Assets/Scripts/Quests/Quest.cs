using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public bool isActive;
    public QuestGoal goal;
    public string title;
    public string description;
    public string neededItem;
}
