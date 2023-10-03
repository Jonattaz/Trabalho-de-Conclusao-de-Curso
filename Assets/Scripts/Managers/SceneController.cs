using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void CloseGame(){
        Application.Quit();
    }

    public void ReloadGame(){
        SceneManager.LoadScene(0);
    }
}











