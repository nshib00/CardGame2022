using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image PlayerArea;
    public GameObject CardPrefab;
    public List<Card> Hand;
    private int PlayerHealth;
        

    public void Awake()
    {
        Hand = new List<Card>();
        PlayerHealth = 20;        
    }


    public void DrawCard(Deck deck)
    {
        Card drawnCard = deck.TopDeck();
        if (drawnCard == null) return;
        Hand.Add(drawnCard);
        GameObject CardToHand = Instantiate(CardPrefab, PlayerArea.transform, false);
        CardToHand.GetComponent<CardInfo>().SelfCard = drawnCard;
        CardToHand.GetComponent<CardInfo>().ShowCardInfo();
    }

}
