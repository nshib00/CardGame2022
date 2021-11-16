using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Text Health;
    public Text Damage;
    public Image Face; 


    public int EnemyHealth { get; private set; }
    public int EnemyDamage { get; private set; }

    public void SetEnemy(int health, int damage, string pict)
    {
        EnemyHealth = health;
        EnemyDamage = damage;
        Face.sprite = Resources.Load<Sprite>(pict);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health != null) Health.text = EnemyHealth.ToString();
        if (Damage != null) Damage.text = EnemyDamage.ToString();
    }
}
