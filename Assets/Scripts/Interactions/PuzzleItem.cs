using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleItem : MonoBehaviour, IInteractable{

    private PuzzleItem item;
    private bool wait;

    [Header("Item Configuration")]
    public bool hasVFX;
    // Verifica se o item interagido é de inventário
    public bool inventoryItem;

    // Mensagem que aparece ao coletar algum item
    public string collectMessage;

    public Text messageTextObj;

    // Prefab correspondente do item
    public GameObject prefab;

    public string itemName;
    
    public string itemType;

    public string itemDescription;
    public GameObject vfxObj;

    // Audio que toca ao interagir com o item
    public AudioClip audioClipOnInteract;
    public Sprite pageImage;

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start(){
        item = GetComponent<PuzzleItem>();
    }

    public void CollectItem(){

        // Adicionar ao inventario da personagem que este item foi coletado
        if (item.inventoryItem && !wait){
            messageTextObj.text = collectMessage;
            StartCoroutine(FadingText());
            if(gameObject.GetComponent<MeshRenderer>() != null){
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            if(hasVFX)
                vfxObj.SetActive(false);
            PlayerInventory.instance.AddItem(item);
            wait = !wait;
        }

    }

    IEnumerator FadingText()
    {
        Color newColor = messageTextObj.color;

        while (newColor.a < 1)
        {
            newColor.a += Time.deltaTime;
            messageTextObj.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        while (newColor.a > 0)
        {
            newColor.a -= Time.deltaTime;
            messageTextObj.color = newColor;
            yield return null;
        }
        wait = !wait;
        this.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Interact(){
        
        CollectItem();
        
    }
}
