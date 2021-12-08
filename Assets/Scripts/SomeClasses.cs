using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardState
{
    NOT_USED,
    IN_DECK,
    IN_HAND,
    IN_PLAY,
    DISCARDED,
    BANISHED,
    WALL
}

public static class IListExtensions
{
    /// <summary>
    /// Шафлим List<T>
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}

public struct ManaCost
{
    public int Light { get; set; }
    public int Materia { get; set; }
    public int Dark { get; set; }

    public ManaCost(int lightMana, int materia, int darkMana)
    {
        Light = lightMana;
        Materia = materia;
        Dark = darkMana;
    }
}

public class Card : ICloneable
{
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public Sprite Illustr { get; private set; }
    public ManaCost SummonCost { get; private set; }
    public ManaCost VoidProfit { get; private set; }
    public CardState State { get; set; }

    public Card(string name, string desc, int health, int damage, string illSRC, ManaCost summonCost, ManaCost voidProfit)
    {
        Name = name;
        Desc = desc;
        Health = health;
        Damage = damage;
        Illustr = Resources.Load<Sprite>(illSRC);
        SummonCost = summonCost;
        VoidProfit = voidProfit;
        State = CardState.NOT_USED;
    }

    public int DamageCard(int damage)
    {
        int ExcessiveDamage = damage - Health;
        if (ExcessiveDamage >= 0) Health = 0;
        else Health -= damage;
        return ExcessiveDamage;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

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
        //Здесь формирование колоды должно быть
        for (int i = 0; i < 20; i++)
            AddCard((Card)AllCards[UnityEngine.Random.Range(0, AllCards.Count)].Clone());
        //Шафлим
        ShuffleDeck();
    }

    public void AddCard(Card newCard)
    {
        if (newCard == null) return;
        Cards.Add(newCard);
        Cards[Cards.Count - 1].State = CardState.IN_DECK;
    }

    public Card TopDeck()
    {
        if (DeckSize == 0)
        {
            //Тоже проигрыш
            return null;
        }

        Card topdeck = Cards[0];
        Cards.RemoveAt(0);
        return topdeck;
    }

    public void ShuffleDeck()
    {
        Cards.Shuffle();
    }
}
