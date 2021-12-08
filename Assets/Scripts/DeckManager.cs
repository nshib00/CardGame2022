using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DeckManager : MonoBehaviour , IPointerClickHandler
{
    public Text DeckSizeCounter;
    public GameObject IPlayer;
    public Deck GameDeck;    

    private void Awake()
    {
        GameDeck = new Deck();
    }

    public void Update()
    {        
        if (DeckSizeCounter != null) DeckSizeCounter.text = GameDeck.DeckSize.ToString();
    }    

    public void OnPointerClick(PointerEventData eventData)
    {        
        Player myPlayer = IPlayer.GetComponent<Player>();
        ManaCost mc = myPlayer.Mana;
        if (mc.Light == 0)
        {
            //Показать нехватку маны для дрова
            return;
        }
        mc.Light--;
        myPlayer.Mana = mc;
        myPlayer.DrawCard(GameDeck);
    }
}
