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
            switch (gameObject.name)
            {           
                case "Void":
                    eventData.pointerDrag.GetComponent<CardInfo>().SelfCard.State = CardState.BANISHED;
                    cm.GoToVoid();
                    break;

                case "PlayedCards":
                    eventData.pointerDrag.GetComponent<CardInfo>().SelfCard.State = CardState.IN_PLAY;
                    break;

                default:
                    break;
            }    
            
        }
            
    }
}
