using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image PlayerArea;
    public GameObject CardPrefab;
    public List<Card> Hand;
    public int PlayerHealth { get; private set; }
    public ManaCost Mana { get; set; }

    public void Awake()
    {
        Hand = new List<Card>();
        PlayerHealth = 20;
        Mana = new ManaCost(1,1,1);

    }

    public void GetHand(int n , Deck deck)
    {
        for (int i = 0; i < n; i++)
            DrawCard(deck);
    }

    public void DrawCard(Deck deck)
    {
        Card drawnCard = deck.TopDeck();
        if (drawnCard == null) return;
        drawnCard.State = CardState.IN_HAND;
        Hand.Add(drawnCard);
        GameObject CardToHand = Instantiate(CardPrefab, PlayerArea.transform, false);
        CardToHand.GetComponent<CardInfo>().FillCardInfo(drawnCard);
    }

    public bool AttackPlayer(int damage)
    {
        PlayerHealth -= damage;
        return (PlayerHealth > 0);
    }

}
