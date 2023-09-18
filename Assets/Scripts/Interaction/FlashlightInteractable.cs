using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightInteractable : Interactable
{  
     [Header("Interaction Options")]
    [SerializeField]
    private float rotateSpeed = 30f;

    [Header("Configurations")]
    [SerializeField]
    private Camera myCamera;

    [SerializeField]
    private Transform objectViewer;

    [SerializeField]
    GameObject flashlightText;

    private Vector3 originPosition;

    private Quaternion originRotation;

    [SerializeField]
    private void Start()
    {
        textController = true;
        interacting = false;
        originPosition = transform.position;
        originRotation = transform.rotation;
    }

    public override void OnFocus()
    {
        if (textController)
        {
            intergirText.SetActive(true);
            textController = false;
            flashlightText.SetActive(true);
            
        }
        if (interacting)
        {
            // Caso o jogo tenha audio/voz usar: Invoke("Interact", item.audioclip.length + 0.5f)
            //FirstPersonController.instance.interacting = true;
            Invoke("Interact", 5f);
            //UI.instaceUI.SetCaptions(item.text);

            //audioPlayer.PlayAudio(item.audioClip);
            RotateObject();
        }
    }

    public override void OnInteract()
    {
        if (!textController) intergirText.SetActive(false);
        if (item.grabbable)
        {
            interacting = true;
            StartCoroutine(MovingObject(this,
            objectViewer.position,
            transform.rotation));
        }
    }

    public override void OnLoseFocus()
    {
        /*if (!FirstPersonController.instance.interacting)
        {
            UI.instaceUI.SetCaptions("");
            interacting = false;
            intergirText.SetActive(false);
            textController = true;
            if (!item.inventoryItem)
            {
                if (!item.flashlight)
                {
                    StartCoroutine(MovingObject(this,
                    originPosition,
                    originRotation));
                }
                else
                {
                    FirstPersonController.instance.canUseFlashlight = true;
                    flashlightText.SetActive(false);
                    Destroy(this.gameObject);

                }
            }
        }
        UI.instaceUI.SetCaptions("");*/
    }

    private void RotateObject()
    {
        float x = Input.GetAxis("Mouse X") * rotateSpeed;
        float y = Input.GetAxis("Mouse Y") * rotateSpeed;
        transform
            .Rotate(myCamera.transform.right,
            -Mathf.Deg2Rad * y * rotateSpeed,
            Space.World);
        transform
            .Rotate(myCamera.transform.up,
            -Mathf.Deg2Rad * x * rotateSpeed,
            Space.World);
    }

    IEnumerator
    MovingObject(FlashlightInteractable obj, Vector3 position, Quaternion rotation)
    {
        float timer = 0;
        while (timer < 1)
        {
            obj.transform.position =
                Vector3
                    .Lerp(obj.transform.position, position, Time.deltaTime * 6);
            timer += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }
}
