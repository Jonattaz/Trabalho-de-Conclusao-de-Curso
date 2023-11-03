using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSceneController : MonoBehaviour{

    public static LocalSceneController instance;
    public float duration;
    public float waitTime;
    /// Awake is called when the script instance is being loaded.
    void Awake(){
        instance = this;
    }

    public void LoadScene(int index){
        GameManager.LoadScene(index, duration, waitTime);
        Debug.Log("Carregando");
    }

    public void CloseGame(){
        GameManager.CloseGame();
    }

}

