using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Card
{
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public Sprite Illustr { get; private set; }
    public int LightCost { get; private set; }
    public int DarkCost { get; private set; }
    public int MateriaCost { get; private set; }

    public Card(string name, string desc, int health, int damage, string illSRC, int lc, int dc, int mc)
    {
        Name = name;
        Desc = desc;
        Health = health;
        Damage = damage;
        Illustr = Resources.Load<Sprite>(illSRC);
        LightCost = lc;
        DarkCost = dc;
        MateriaCost = mc;
    }
}

public class Game : MonoBehaviour
{
    public GameObject player;
    public GameObject CardPrefab;
    public GameObject ScrDeck;
    private List<Card> AllCards;
    



    // Start is called before the first frame update
    public void Awake()
    {
        AllCards = new List<Card>();
        AllCards.Add(new Card("Чемпион", "Несущая свет", 5, 4, "Sprites/Cards/Champ", 2, 0, 2));
        AllCards.Add(new Card("Белый Дракон", "Дыхание Бога", 7, 2, "Sprites/Cards/WDrag", 4, 0, 3));
        AllCards.Add(new Card("Имп", "Жар преисподней", 2, 3, "Sprites/Cards/Imp", 0, 1, 1));

    }

    // Update is called once per frame
    void Update()
    {       

    }

    public void Start()
    {
        Deck myDeck = ScrDeck.GetComponent<DeckManager>().GameDeck;
        Player myPlayer = player.GetComponent<Player>();
        myDeck.FormDeck(AllCards);
        for (int i = 0; i < 3; i++)
            myPlayer.DrawCard(myDeck);
    }

    public void DeckClick(BaseEventData pointerEventData)
    {
        Deck myDeck = ScrDeck.GetComponent<DeckManager>().GameDeck;
        Player myPlayer = player.GetComponent<Player>();
        Debug.Log(myPlayer);
        myPlayer.DrawCard(myDeck);
    }

    

   

}