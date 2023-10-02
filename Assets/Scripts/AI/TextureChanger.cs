using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour{

    [SerializeField] private Renderer renderer;
    [SerializeField] private Texture[] textures;
    [SerializeField] private float TimeChanger;
    [SerializeField] private int index;
    [SerializeField] private float RemainingTime;


    // Start is called before the first frame update
    void Start(){
        RemainingTime = TimeChanger;
        index = 0;
        renderer.material.SetTexture("_MainTex", textures[index]);    
    }

    // Update is called once per frame
    void Update(){

        ChangeTexture();
        
    }

    private void ChangeTexture(){
        
        if(RemainingTime <= 0){
            RemainingTime = TimeChanger;
            index++;
            if(index >= textures.Length){
                index = 0;
            }

            renderer.material.SetTexture("_MainTex", textures[index]);
        }else{
            RemainingTime -= Time.deltaTime;
        }
    }
}
