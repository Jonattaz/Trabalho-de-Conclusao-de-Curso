using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    [Header("Item configuration")]
    // Controla se o objeto é pegavél ou não
    public bool grabbable;

    // Audio que toca ao interagir com o item
    public AudioClip audioClip;

    // Texto que aparece ao interagir com o item
    public string text;

    [Header("Inventoy")]
    // Verifica se o item interagido é de inventário
    public bool inventoryItem;

    // Mensagem que aparece ao coleta algum item
    public string collectMessage;

    // Prefab correspondente do item
    public GameObject prefab;

    // Bool que serve para identificar um item de quest
    public string itemName;

    public bool flashlight;
}
