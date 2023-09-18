using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : Interactable
{
    [Header("Interaction Options")]
    [SerializeField]
    private float rotateSpeed = 30f;

    [Header("Configurations")]
    [SerializeField]
    private Camera myCamera;

    [SerializeField]
    private Transform objectViewer;

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
        if (!textController)
            intergirText.SetActive(false);
        if (item.grabbable)
        {
            interacting = true;
            StartCoroutine(MovingObject(this, objectViewer.position, transform.rotation));
        }
    }

    public override void OnLoseFocus()
    {
        /*if (!FirstPersonController.instance.interacting)
        {
            //UI.instaceUI.SetCaptions("");
            interacting = false;
            intergirText.SetActive(false);
            textController = true;
            if (!item.inventoryItem)
            {
                StartCoroutine(MovingObject(this, originPosition, originRotation));
            }
        }
        UI.instaceUI.SetCaptions("");*/
    }

    private void RotateObject()
    {
        float x = Input.GetAxis("Mouse X") * rotateSpeed;
        float y = Input.GetAxis("Mouse Y") * rotateSpeed;
        transform.Rotate(myCamera.transform.right, -Mathf.Deg2Rad * y * rotateSpeed, Space.World);
        transform.Rotate(myCamera.transform.up, -Mathf.Deg2Rad * x * rotateSpeed, Space.World);
    }

    IEnumerator MovingObject(InteractableObject obj, Vector3 position, Quaternion rotation)
    {
        float timer = 0;
        while (timer < 1)
        {
            obj.transform.position = Vector3.Lerp(
                obj.transform.position,
                position,
                Time.deltaTime * 6
            );
            timer += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }
}
