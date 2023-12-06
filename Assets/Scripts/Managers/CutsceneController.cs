using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour{

    public bool timerIsRunning;
    private float timer;
    [SerializeField] private GameObject cutsceneCanvas;
    [SerializeField] private GameObject sceneActivator;
    // Animatic 40 seconds
    [SerializeField] private float cutsceneSeconds;
    [SerializeField] private bool introCutscene;

    // Start is called before the first frame update
    void Start(){
        if(introCutscene){
            timerIsRunning = true;
        }
    }

    // Update is called once per frame
    void Update(){
        if(timerIsRunning)
            EndCutscene();        
    }

    public void SkipCutscene(){
        sceneActivator.SetActive(true);
        Time.timeScale = 1;
        Cursor.visible = false;
        cutsceneCanvas.SetActive(false);
    }

    private void EndCutscene(){ 
        
        if (timer < cutsceneSeconds){
            timer += Time.deltaTime;
        }else{
            timer = 0;
            SkipCutscene();
            timerIsRunning = false;
        }
    }
}
