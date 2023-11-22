using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWarning : MonoBehaviour{

    // Mensagem que aparece ao coletar algum item
    public string collectMessage;

    public Text messageTextObj;

    public float textTime;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    // OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other){
         messageTextObj.text = collectMessage;
        StartCoroutine(FadingText());    
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

        yield return new WaitForSeconds(textTime);

        while (newColor.a > 0)
        {
            newColor.a -= Time.deltaTime;
            messageTextObj.color = newColor;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}






