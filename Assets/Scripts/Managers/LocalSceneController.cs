using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LocalSceneController : MonoBehaviour{

    private AudioSource audioSource;
    public static LocalSceneController instance;
    public float duration;
    public float waitTime;
    public AudioClip clickSound;
    public bool hasClickSound;
    
    /// Awake is called when the script instance is being loaded.
    void Awake(){
        instance = this;
    }

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start(){
        
        audioSource = GetComponent<AudioSource>();
        // Create a temporary reference to the current scene.
	    Scene currentScene = SceneManager.GetActiveScene();

	    // Retrieve the name of this scene.
	    string sceneName = currentScene.name;

		if (sceneName == "Menu"){
			Cursor.visible = true;
		}
	
    }

    public void LoadScene(int index){
        if(hasClickSound)
            audioSource.PlayOneShot(clickSound);

        GameManager.LoadScene(index, duration, waitTime);
        Debug.Log("Carregando");
    }

    public void CloseGame(){
        if(hasClickSound)
            audioSource.PlayOneShot(clickSound);
        GameManager.CloseGame();
    }

}

