using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectThrowItem : MonoBehaviour, IInteractable{   
    
    [SerializeField] private GameObject model;
    [SerializeField] private bool thrown;
    [SerializeField] private bool wait;
    [SerializeField] private float destroyTime;
    [SerializeField] ThrowItem throwItemRef;
    // Mensagem que aparece ao coletar algum item
    public string collectMessage;
    public Text messageTextObj;


    public void CollectItem(){
        if(!wait){
            messageTextObj.text = collectMessage;
            StartCoroutine(FadingText());
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            model.SetActive(false);
            wait = !wait;
        }
    }

    public void Interact(){
        if(!thrown)
            CollectItem();
    }


    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision other){
        if(thrown){
            if(other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy"){
                HearingManager.Instance.OnSoundEmitted(gameObject, transform.position, EHeardSoundCategory.EItem, 2f);
                Destroy(this.gameObject, destroyTime);
            }
        }
    }

    IEnumerator FadingText()
    {
        throwItemRef.totalThrows++;
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
        
        thrown = true;
        wait = !wait;
        Destroy(this.gameObject);
    }
}
