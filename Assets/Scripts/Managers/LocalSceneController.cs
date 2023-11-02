using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSceneController : MonoBehaviour{

    public static LocalSceneController instance;

    /// Awake is called when the script instance is being loaded.
    void Awake(){
        instance = this;
    }

    public void LoadScene(int index){
        GameManager.LoadScene(index, 1, 2);
    }

    public void CloseGame(){
        GameManager.CloseGame();
    }

}

