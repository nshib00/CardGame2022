using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public Text CardName;
    public Text CardDescription;
    public Text CardHealth;
    public Text CardDamage;
    public Image CardImage;
    public Image Cost;

    public Card SelfCard;

    private void LoadCost(string src)
    {
        GameObject NewObj = new GameObject(); 
        Image NewImage = NewObj.AddComponent<Image>(); 
        NewImage.sprite = Resources.Load<Sprite>(src); 
        RectTransform trans = NewImage.GetComponent<RectTransform>();
        trans.transform.SetParent(Cost.transform); 
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0f, 0f); 
        trans.sizeDelta = new Vector2(Cost.rectTransform.rect.height - 2, Cost.rectTransform.rect.height - 2);
        trans.localPosition = Vector3.zero;
    }

    public void ShowCardInfo()
    {        
        CardName.text = SelfCard.Name;
        CardDescription.text = SelfCard.Desc;
        CardHealth.text = SelfCard.Health.ToString();
        CardDamage.text = SelfCard.Damage.ToString();
        Debug.Log(SelfCard.Illustr);
        CardImage.sprite = SelfCard.Illustr;
        for (int i = 0; i < SelfCard.LightCost; i++) LoadCost("Sprites/Interface/Light");
        for (int i = 0; i < SelfCard.DarkCost; i++) LoadCost("Sprites/Interface/Dark");
        for (int i = 0; i < SelfCard.MateriaCost; i++) LoadCost("Sprites/Interface/Materia");

    }
}
