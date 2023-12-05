using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public Image fadeImage;

    private static GameManager GameManagerInstance;

    // Awake is called when the script instance is being loaded.
    void Awake(){
        
        if(GameManagerInstance == null){

            GameManagerInstance = this;
            DontDestroyOnLoad(gameObject);

            fadeImage.rectTransform.sizeDelta = new Vector2(Screen.width + 20, Screen.height + 20);
            fadeImage.gameObject.SetActive(false);

        }else{
            
            Destroy(gameObject);

        }
        
    }

    /* Start is called on the frame when a script is enabled just before
        any of the Update methods is called the first time.*/
    void Start(){
        
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update(){
        
    }

    public static void LoadScene(int index, float duration = 1, float waitTime = 0){

        //GameManagerInstance.StartCoroutine(GameManagerInstance.FadeScene(index, duration, waitTime));
        //AsyncOperation ao = SceneManager.LoadSceneAsync(index);
        SceneManager.LoadScene(index);

    }

    private IEnumerator FadeScene(int index, float duration, float waitTime){

        fadeImage.gameObject.SetActive(true);

        for(float t = 0; t < 1; t+= Time.deltaTime / duration){
            
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;

        }

        AsyncOperation ao = SceneManager.LoadSceneAsync(index);

        while(!ao.isDone){
            yield return null;

        } 

        yield return new WaitForSeconds(waitTime);

        for(float t = 0; t < 1; t += Time.deltaTime / duration){
            
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t));
            yield return null;

        }

        fadeImage.gameObject.SetActive(false);

    }

    public static void CloseGame(){
        Application.Quit();
    }
}











