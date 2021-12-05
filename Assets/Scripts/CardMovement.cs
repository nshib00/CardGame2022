using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour , IBeginDragHandler, IEndDragHandler , IDragHandler, IPointerEnterHandler , IPointerExitHandler
{
    private Camera mainCamera;

    private Vector3 CardPosition;
    private Vector3 offset;
    public Transform DefaultParent;
    CardState cardState;

    public void Awake()
    {
        mainCamera = Camera.allCameras[0];
    }

    public void Update()
    {
        Card SelfCard = gameObject.GetComponent<CardInfo>().SelfCard;
        cardState = SelfCard.State;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (cardState != CardState.IN_HAND) return;
        offset = transform.position - mainCamera.ScreenToWorldPoint(eventData.position);
        DefaultParent = transform.parent;
        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cardState != CardState.IN_HAND) return;
        CardPosition = mainCamera.ScreenToWorldPoint(eventData.position) + offset;
        Debug.Log(CardPosition);
        transform.position = CardPosition; //CardPosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cardState != CardState.IN_HAND) return;
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardState != CardState.IN_HAND) return;
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("In", true);
        anim.SetBool("Out", false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cardState != CardState.IN_HAND) return;
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("In", false);
        anim.SetBool("Out", true);
    }

    public void GoToVoid()
    {
        
    }
}
