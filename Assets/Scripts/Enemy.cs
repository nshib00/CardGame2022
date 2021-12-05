using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Text Health;
    public Text Damage;
    public Text EName;
    public Text EDesc;
    public Image Face;
    public Image Army;


    public int EnemyHealth { get; private set; }
    public int EnemyDamage { get; private set; }
    public string EnemyName { get; private set; }
    public string EnemyDescription { get; private set; }

    private int initHealth;

    public void SetEnemy(int health, int damage, string pict, string name, string desc)
    {
        EnemyHealth = health;
        EnemyDamage = damage;
        Face.sprite = Resources.Load<Sprite>(pict);
        EnemyName = name;
        EnemyDescription = desc;
        if (EName != null) EName.text = EnemyName;
        if (EDesc != null) EDesc.text = EnemyDescription;
        Face.material.SetFloat("Power", 0f);

        initHealth = health;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health == null) return;
        if (Health != null) Health.text = EnemyHealth.ToString();
        if (Damage != null) Damage.text = EnemyDamage.ToString();
        

    }

    public void GetHit(int hit)
    {        
        EnemyHealth -= hit;
        if (EnemyHealth < 0)        
            EnemyHealth = 0;           
        float clr = 1f - (float)EnemyHealth / (float)initHealth;
        Face.material.SetFloat("Power", clr);
    }
    
}
