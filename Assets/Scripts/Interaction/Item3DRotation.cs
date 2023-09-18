using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item3DRotation : MonoBehaviour, IDragHandler
{
    private GameObject itemPrefab;

    public void OnDrag(PointerEventData eventData)
    {
        itemPrefab = PlayerInventory.instance.itemPrefab;
        itemPrefab.transform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
    }
}
