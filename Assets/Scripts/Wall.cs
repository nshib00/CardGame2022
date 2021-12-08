using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject Army;
    public GameObject CardPrefab;

    public void CreateWall()
    {
        GameObject CurrentWall = transform.GetChild(0).gameObject;
        if (CurrentWall.GetComponent<CardInfo>().PlayToBattlefield())
        {            
            CurrentWall.transform.SetParent(Army.transform);
            Army.transform.GetChild(Army.transform.childCount - 1).SetAsFirstSibling();
        }
        else return;
        GameObject NewWall = Instantiate(CardPrefab, transform, false);
        NewWall.GetComponent<CardInfo>().FillCardInfo((Card)(CurrentWall.GetComponent<CardInfo>().SelfCard.Clone()));
        NewWall.GetComponent<CardInfo>().SelfCard.State = CardState.WALL;
    }
}
