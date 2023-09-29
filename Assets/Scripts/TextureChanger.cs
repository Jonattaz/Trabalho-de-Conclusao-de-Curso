using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour{

    [SerializeField] private KeyCode changeKey = KeyCode.Space;
    [SerializeField] private Texture[] textures;
    [SerializeField] private int index;
    private Renderer renderer;


    // Start is called before the first frame update
    void Start(){
        index = 0;
        renderer = GetComponent<Renderer>();
        renderer.material.SetTexture("_MainTex", textures[index]);    
    }

    // Update is called once per frame
    void Update(){

        if(Input.GetKeyDown(changeKey)){
         
            ChangeTexture();
        }

    }

    private void ChangeTexture(){
        index++;
        if(index >= textures.Length){
            index = 0;
        }

        renderer.material.SetTexture("_MainTex", textures[index]);
    }
}
