using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNote : Interactable
{
    // Sprite que aparece na tela ao interagir com um texto
    public Sprite imageNote;

    // Start is called before the first frame update
    void Start()
    {
        textController = true;
        interacting = false;

    }
    public override void OnFocus()
    {
        if (textController)
        {
            intergirText.SetActive(true);
            textController = false;
        }

        if (interacting)
        {
            // Caso o jogo tenha audio/voz usar: Invoke("Interact", item.audioclip.length + 0.5f)
            //FirstPersonController.instance.interacting = true;
            Interact();
            //UI.instaceUI.SetCaptions(item.text);
            //audioPlayer.PlayAudio(item.audioClip);
        }
    }

    public override void OnInteract()
    {
        if (imageNote != null)
        {
            interacting = true;
            //UI.instaceUI.SetImage(imageNote);
        }
        else
        {
            print("Você esqueceu de colocar a imagem no item");
        }
    }

    public override void OnLoseFocus()
    {
        /*if (!FirstPersonController.instance.interacting)
        {
            UI.instaceUI.SetBackImage(false);
            UI.instaceUI.SetCaptions("");
            interacting = false;
            intergirText.SetActive(false);
            textController = true;
        }*/
    }

}
