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

    public void SetPlayer(int SetHealth, int SetLight, int SetMateria, int SetDark)
    {
        Hand = new List<Card>();
        PlayerHealth = SetHealth;
        Mana = new ManaCost(SetLight, SetMateria, SetDark);
    }

    public void GetHand(int n , Deck deck)
    {
        for (int i = 0; i < n; i++)
            DrawCard(deck);
    }

    public void DrawCard(Deck deck)
    {
        Card drawnCard = deck.TopDeck();
        if (drawnCard == null)
        {
            //Тут тоже проигрыш
            return;
        }
        drawnCard.State = CardState.IN_HAND;
        Hand.Add(drawnCard);
        GameObject CardToHand = Instantiate(CardPrefab, PlayerArea.transform, false);
        CardToHand.GetComponent<CardInfo>().FillCardInfo(drawnCard);
    }

    public void AttackPlayer(int damage)
    {
        PlayerHealth -= damage;
        if (PlayerHealth < 0) PlayerHealth = 0;
    }

    public void AddMana(int addedLight, int addedMateria, int addedDark)
    {
        ManaCost mana = Mana;
        mana.Materia += addedLight;
        mana.Light += addedMateria;
        mana.Dark += addedDark;
        Mana = mana;
    }

}
