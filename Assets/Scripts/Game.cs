using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject IPlayer;
    public GameObject IDeck;
    public GameObject IEnemy;
    public Image Army;
    public Button EndTurnButton;
    public GameObject ScratchPrefab;
    
    private List<Card> AllCards;
    private bool endTurn = false;
    
    /// <summary>
    /// Функция создания галереи всех доступных карт
    /// </summary>
    private void LoadCardGallery()
    {
        AllCards.Add(new Card("Чемпион", "Несущая свет", 5, 4, "Sprites/Cards/Champ", new ManaCost(2, 2, 0) , new ManaCost (2,1,0)));
        AllCards.Add(new Card("Белый Дракон", "Дыхание Бога", 7, 2, "Sprites/Cards/WDrag", new ManaCost(4, 3, 0) , new ManaCost(3, 0, 0)));
        AllCards.Add(new Card("Имп", "Жар преисподней", 2, 3, "Sprites/Cards/Imp", new ManaCost(0, 1, 1) , new ManaCost(0, 0, 2)));
        AllCards.Add(new Card("Маг", "Жаждущий знаний", 1, 5, "Sprites/Cards/Mage", new ManaCost(1, 2, 1) , new ManaCost(0, 3, 0)));
    }

    // Start is called before the first frame update
    public void Awake()
    {
        AllCards = new List<Card>();
        LoadCardGallery();
    }
    

    public void Start()
    {
        Deck myDeck = IDeck.GetComponent<DeckManager>().GameDeck;
        Player myPlayer = IPlayer.GetComponent<Player>();
        myDeck.FormDeck(AllCards);
        myPlayer.GetHand(5,myDeck);
        IEnemy.GetComponent<Enemy>().SetEnemy(40, 5, "Sprites/Cards/Boss_Destroyer", "Разрушитель миров", "Воплощение ярости");
    }

    public void EndPlayerTurn()
    {
        endTurn = false;
        StartCoroutine(RoundEnd());
    }

    private IEnumerator RoundEnd()
    {
        Cursor.visible = false;
        EndTurnButton.enabled = false;
        Enemy enemy = IEnemy.GetComponent<Enemy>();
        Player player = IPlayer.GetComponent<Player>();
        EndTurnButton.GetComponentInChildren<Text>().text = "Армия атакует";        
        
        for (int i = 0; i < Army.transform.childCount; i++)
        {            
            //Применение свойств до атаки
            GameObject Attacker = Army.transform.GetChild(i).gameObject;
            yield return StartCoroutine(SpawnHit(Attacker.transform.position,IEnemy.transform.position));            
            enemy.GetHit(Attacker.GetComponent<CardInfo>().SelfCard.Damage);            
            //Применение свойств после атаки
        }
        if (enemy.EnemyHealth == 0)
        {
            //Празднуем победу
        }
        EndTurnButton.GetComponentInChildren<Text>().text = "Босс атакует";
        
        //Примененние свойств до атаки (Общее)        
        int AttackValue = enemy.EnemyDamage;
        int armyCount = Army.transform.childCount;
        for (int i = 0; i < armyCount; i++)
        {
            //Применение свойств до атаки
            GameObject Attacker = Army.transform.GetChild(0).gameObject;
            yield return StartCoroutine(SpawnHit(IEnemy.transform.position, Attacker.transform.position));
            AttackValue = Attacker.GetComponent<CardInfo>().GetHit(AttackValue);  
            if (AttackValue <= 0) break;
            //Применение свойств после атаки

        }
        //Примененние свойств после атаки (Общее) 
        if (AttackValue > 0) player.AttackPlayer(AttackValue);
        yield return new WaitForSeconds(2);
        if (player.PlayerHealth == 0)
        {
            //Горечь поражения
        }
        EndTurnButton.GetComponentInChildren<Text>().text = "Закончить ход";
        EndTurnButton.enabled = true;

        Cursor.visible = true;

    }

    private IEnumerator SpawnHit(Vector3 spawn, Vector3 target)
    {
        GameObject mainCanvas = GameObject.Find("Canvas");
        spawn = new Vector3(spawn.x, spawn.y, 91);
        target = new Vector3(target.x, target.y, 91);
        GameObject AttackScratch = Instantiate(ScratchPrefab, spawn, Quaternion.identity, mainCanvas.transform);         
        yield return StartCoroutine(MoveAtSpeedCoroutine(AttackScratch, target, 10));
    }


    private IEnumerator MoveAtSpeedCoroutine(GameObject s, Vector3 end, float speed)
    {
        while (Vector3.Distance(s.transform.position, end) > speed * Time.deltaTime)
        {
            Debug.Log("Префаб  в позиции" + s.transform.position);
            s.transform.position = Vector3.MoveTowards(s.transform.position, end, speed * Time.deltaTime);
            yield return null;
        }
        s.transform.position = end;
        Destroy(s);
    }
}