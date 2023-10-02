using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour{

    [SerializeField] private AudioSource[] AudioSources;
    [SerializeField] private int index;
    // Start is called before the first frame update
    void Start(){
        index = 0;    
    }

    // Update is called once per frame
    void Update(){

        StartCoroutine(PlaySound());        
    }

     IEnumerator PlaySound(){
        AudioSources[index].Play();
        yield return new WaitForSeconds(AudioSources[index].clip.length);
        AudioSources[index].Stop();
        index++;
        if(index >= AudioSources.Length){
            index = 0;
        }
        AudioSources[index].Play();
    }
}
