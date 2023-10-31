using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                this.gameObject.SetActive(false);
                PlayerInventory.instance.AddItem(item);
        }

    }

    public void Interact(){
        
        CollectItem();
        
    }
}
