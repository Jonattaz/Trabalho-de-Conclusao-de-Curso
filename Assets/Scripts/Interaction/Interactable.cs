using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject intergirText;
    [SerializeField] protected Item item;
    [SerializeField] protected bool textController;
    [SerializeField] protected bool interacting;

    public virtual void Awake()
    {
        // Número da layer que possui como nome Interactble 
        gameObject.layer = 6;
    }

    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();

    public void Interact()
    {
        //FirstPersonController.instance.interacting = false; - Referência a bool do player que controla se ele está interagindo com algo
        if (item.inventoryItem)
            {
                this.gameObject.SetActive(false);
                PlayerInventory.instance.AddItem(item);
            }
    }
}
