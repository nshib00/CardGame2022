using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardMovement cm = eventData.pointerDrag.GetComponent<CardMovement>();        
        if (cm)
        {            
            cm.DefaultParent = gameObject.transform;
            if (gameObject.name == "Void") cm.GoToVoid();
            
        }
            
    }
}
