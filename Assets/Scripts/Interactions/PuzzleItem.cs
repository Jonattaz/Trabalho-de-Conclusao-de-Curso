using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleItem : MonoBehaviour, IInteractable{

     [Header("Item configuration")]

    // Audio que toca ao interagir com o item
    public AudioClip audioClip;

    private PuzzleItem item;

    // Texto que aparece ao interagir com o item
    public string text;

    [Header("Inventoy")]
    // Verifica se o item interagido é de inventário
    public bool inventoryItem;

    // Mensagem que aparece ao coletar algum item
    public string collectMessage;

    public Text messageTextObj;

    // Prefab correspondente do item
    public GameObject prefab;

    // Bool que serve para identificar um item de quest
    public string itemName;


    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start(){
        item = GetComponent<PuzzleItem>();
    }

    public void CollectItem(){

        // Adicionar ao inventario da personagem que este item foi coletado
        if (item.inventoryItem){
                messageTextObj.text = collectMessage;
                StartCoroutine(FadingText());
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

        
        PlayerInventory.instance.AddItem(item);
        this.gameObject.SetActive(false);
    }

    public void Interact(){
        
        CollectItem();
        
    }
}
