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
    public Text CardMateria;
    public Text CardLightCost;
    public Text CardDarkCost;
    public Image VoidProfit;

    public Card SelfCard;

    private void Update()
    {
        if (CardHealth != null) CardHealth.text = SelfCard.Health.ToString();
    }

    //Динамическое добавление иконки маны на карту
    private void ShowCost(string src , Image ParentImage)
    {
        GameObject NewObj = new GameObject(); 
        Image NewImage = NewObj.AddComponent<Image>(); 
        NewImage.sprite = Resources.Load<Sprite>(src); 
        RectTransform trans = NewImage.GetComponent<RectTransform>();
        trans.transform.SetParent(ParentImage.transform); 
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0f, 0f); 
        trans.sizeDelta = new Vector2(ParentImage.rectTransform.rect.height - 2, ParentImage.rectTransform.rect.height - 2);
        trans.localPosition = Vector3.zero;
    }

    public void FillCardInfo(Card card)
    {
        SelfCard = card;
        CardName.text = SelfCard.Name;
        CardDescription.text = SelfCard.Desc;
        CardHealth.text = SelfCard.Health.ToString();
        CardDamage.text = SelfCard.Damage.ToString();        
        CardImage.sprite = SelfCard.Illustr;
        CardMateria.text = SelfCard.SummonCost.Materia.ToString();
        CardLightCost.text = SelfCard.SummonCost.Light.ToString();
        CardDarkCost.text = SelfCard.SummonCost.Dark.ToString();       
        for (int i = 0; i < SelfCard.VoidProfit.Light; i++) ShowCost("Sprites/Interface/Light", VoidProfit);
        for (int i = 0; i < SelfCard.VoidProfit.Materia; i++) ShowCost("Sprites/Interface/Materia", VoidProfit);
        for (int i = 0; i < SelfCard.VoidProfit.Dark; i++) ShowCost("Sprites/Interface/Dark", VoidProfit);
    }

    public bool BanishToVoid()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        ManaCost mc = player.Mana;
        if (mc.Dark == 0) return false;        
        SelfCard.State = CardState.BANISHED;        
        mc.Dark--;
        mc.Light += SelfCard.VoidProfit.Light;
        mc.Materia += SelfCard.VoidProfit.Materia;
        mc.Dark += SelfCard.VoidProfit.Dark;
        player.Mana = mc;

        //Часть с активацией свойств при жертве карты

        Animator anim = GetComponent<Animator>();
        anim.SetBool("Void", true);
        Destroy(gameObject, 0.95f);
        return true;
    }

    public bool PlayToBattlefield()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        ManaCost mc = player.Mana;
        bool lowMana = false;
        if (mc.Light < SelfCard.SummonCost.Light)
        {            
            Animation animation = GameObject.Find("LightMana").GetComponent<Animation>();
            animation.Play("Low");
            lowMana = true;
        }            
        if (mc.Materia < SelfCard.SummonCost.Materia)
        {            
            Animation animation = GameObject.Find("Materia").GetComponent<Animation>();
            animation.Play("LowMateria");
            lowMana = true;
        }
        if (mc.Dark < SelfCard.SummonCost.Dark)
        {
            Animation animation = GameObject.Find("DarkMana").GetComponent<Animation>();//ищем аниматор
            animation.Play("LowDark");//включена анимация недостатка темной маны 
            lowMana = true;
        }
        if (lowMana) return false;
        mc.Light -= SelfCard.SummonCost.Light;
        mc.Materia -= SelfCard.SummonCost.Materia;
        mc.Dark -= SelfCard.SummonCost.Dark;

        player.Mana = mc;

        SelfCard.State = CardState.IN_PLAY;

        //Часть с активацией свойств при розыгрыше карты

        return true;

    }

    public int GetHit(int hit)
    {
        int ExcessiveDamage = SelfCard.DamageCard(hit);
        if (ExcessiveDamage >= 0)
            SelfCard.State = CardState.DISCARDED;        
        return ExcessiveDamage;
    }

}
