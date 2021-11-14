using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck
{
    private List<Card> Cards;
    public int DeckSize => Cards.Count;
    
    public Deck()
    {
        Cards = new List<Card>();
    }

    public void FormDeck(List<Card> AllCards)
    {
        AddCard(AllCards[0]);
        AddCard(AllCards[0]);
        AddCard(AllCards[1]);
        AddCard(AllCards[2]);
        AddCard(AllCards[1]);
    }

    public void AddCard(Card newCard)
    {
        Cards.Add(newCard);
    }

    public Card TopDeck()
    {
        if (DeckSize == 0) return null;
        Card topdeck = Cards[0];
        Cards.RemoveAt(0);
        return topdeck;
    }



}

public class DeckManager : MonoBehaviour
{
    public Text DeckSizeCounter;

    public Deck GameDeck;

    private void Awake()
    {
        GameDeck = new Deck();
    }

    public void Update()
    {
        //DeckSizeCounter.text = "1";
        //Debug.Log("Размер колоды " + GameDeck.DeckSize.ToString());
        if (DeckSizeCounter != null) DeckSizeCounter.text = GameDeck.DeckSize.ToString();

    }
}
