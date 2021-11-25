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
            switch (gameObject.name)
            {           
                case "Void":

                    if (eventData.pointerDrag.GetComponent<CardInfo>().BanishToVoid())                   
                        cm.DefaultParent = gameObject.transform.parent;  
                    break;

                case "PlayedCards":
                    if (eventData.pointerDrag.GetComponent<CardInfo>().PlayToBattlefield())
                        cm.DefaultParent = gameObject.transform;
                    break;
                default:
                    break;
            }    
            
        }
            
    }
}
