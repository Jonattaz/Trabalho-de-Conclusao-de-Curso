using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSceneController : MonoBehaviour{


    public void LoadScene(int index){
        GameManager.LoadScene(index, 1, 2);
    }

    public void CloseGame(){
        GameManager.CloseGame();
    }

}

