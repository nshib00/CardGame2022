using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject IPlayer;
    public GameObject IDeck;
    public GameObject IEnemy;
    public Image Army;
    public Button EndTurnButton;
    public GameObject ScratchPrefab;
    public GameObject WallSpawn;
    public GameObject CardPrefab;
    public Text win_label;
    public Button resetGame;

    private List<Card> AllCards;
    private Card WallCard;
    
    /// <summary>
    /// Функция создания галереи всех доступных карт
    /// </summary>
    private void LoadCardGallery()
    {
        //Загрузка колоды
        AllCards.Add(new Card("Чемпион", "Несущая свет", 4, 3, "Sprites/Cards/Champ", new ManaCost(2, 2, 0) , new ManaCost (2,1,0)));

        AllCards.Add(new Card("Гений мысли", "Глаз правосудия", 8, 17, "Sprites/Cards/Scala", new ManaCost(6, 8, 7), new ManaCost(6, 7, 7)));
        AllCards.Add(new Card("Резняк", "Собаколюб", 2, 3, "Sprites/Cards/Johnkick", new ManaCost(1, 2, 1), new ManaCost(1, 1, 1)));
        AllCards.Add(new Card("Керальд", "Проиграл квартиру в гвинт", 1, 7, "Sprites/Cards/geralt", new ManaCost(1, 1, 1), new ManaCost(1, 0, 1)));

        AllCards.Add(new Card("Белый Дракон", "Дыхание Бога", 7, 2, "Sprites/Cards/WDrag", new ManaCost(4, 3, 0) , new ManaCost(3, 0, 0)));
        AllCards.Add(new Card("Имп", "Жар преисподней", 2, 3, "Sprites/Cards/Imp", new ManaCost(0, 1, 2) , new ManaCost(0, 0, 3)));
        AllCards.Add(new Card("Маг", "Жаждущий знаний", 2, 5, "Sprites/Cards/Mage", new ManaCost(1, 2, 1) , new ManaCost(0, 3, 0)));
        AllCards.Add(new Card("Чертёнок", "Малое зло", 1, 1, "Sprites/Cards/LittleHorn", new ManaCost(0, 0, 1), new ManaCost(0, 0, 2)));
        AllCards.Add(new Card("Королева бездны", "Меч тьмы", 3, 6, "Sprites/Cards/PitLord", new ManaCost(1, 1, 5), new ManaCost(0, 1, 2)));
        AllCards.Add(new Card("Снежная стая", "Вихрь стрел и клыков", 2, 2, "Sprites/Cards/SnowPack", new ManaCost(1, 1, 1), new ManaCost(1, 1, 1)));
        //Особая карта Стенки
        WallCard = new Card("Стена", "На грани миров", 2, 0, "Sprites/Cards/Wall", new ManaCost(0, 1, 0), new ManaCost(0, 0, 0));
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
        myPlayer.SetPlayer(20, 1, 1, 1);
        myPlayer.GetHand(5, myDeck);
        GameObject Wall = Instantiate(CardPrefab, WallSpawn.transform, false);
        Wall.GetComponent<CardInfo>().FillCardInfo((Card)WallCard.Clone());
        IEnemy.GetComponent<Enemy>().SetEnemy(40, 5, "Sprites/Cards/Boss_Destroyer", "Разрушитель миров", "Воплощение ярости");
    }

    public void EndPlayerTurn()
    {
        StartCoroutine(RoundEnd());
    }

    //корутина конца хода игрока: боевка, атака босса и подготовка к следующему раунду
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
                enemy.GetHit(Attacker.GetComponent<CardInfo>().SelfCard.Damage);            
                if(Attacker.GetComponent<CardInfo>().SelfCard.Damage > 0)
                {
                    yield return StartCoroutine(SpawnHit(Attacker.transform.position, IEnemy.transform.position));
                }
                //Применение свойств после атаки
            }

        if (enemy.EnemyHealth == 0)
        {
            GameVictory();
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
            if (Attacker.GetComponent<CardInfo>().SelfCard.State == CardState.DISCARDED)
            {
                Transform discard = GameObject.Find("DiscardPile").transform;
                yield return StartCoroutine(MoveAtSpeedCoroutine(Attacker, discard.position, 15.0f));
                Attacker.transform.SetParent(discard);
            }
            //Применение свойств после атаки
            if (AttackValue <= 0) break;      
        }
        //Примененние свойств после атаки (Общее) 
        if (AttackValue > 0) player.AttackPlayer(AttackValue);
        yield return new WaitForSeconds(2);
        if (player.PlayerHealth == 0)
        {
            //Горечь поражения
        }

        //Подготовка к новому раунду
        EndTurnButton.GetComponentInChildren<Text>().text = "Фаза ресурсов";
        //Тут должны быть броски кубиков для опрделения притока ресурсов
        //Пока по 1 всего
        player.AddMana(1, 1, 1);
        //Добрать 1 карту
        player.DrawCard(IDeck.GetComponent<DeckManager>().GameDeck);

        EndTurnButton.GetComponentInChildren<Text>().text = "Закончить ход";
        EndTurnButton.enabled = true;

        Cursor.visible = true;

    }

    //Корутина спавна удара
    private IEnumerator SpawnHit(Vector3 spawn, Vector3 target)
    {
        GameObject mainCanvas = GameObject.Find("Canvas");
        spawn = new Vector3(spawn.x, spawn.y, 91);
        target = new Vector3(target.x, target.y, 91);
        GameObject AttackScratch = Instantiate(ScratchPrefab, spawn, Quaternion.identity, mainCanvas.transform);         
        yield return StartCoroutine(MoveAtSpeedCoroutine(AttackScratch, target, 10));
        Destroy(AttackScratch);
    }

    //Корутина движения объекта
    private IEnumerator MoveAtSpeedCoroutine(GameObject s, Vector3 end, float speed)
    {
        while (Vector3.Distance(s.transform.position, end) > speed * Time.deltaTime)
        {
            s.transform.position = Vector3.MoveTowards(s.transform.position, end, speed * Time.deltaTime);
            yield return null;
        }
        s.transform.position = end;
    }


    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameVictory()
    {
        Debug.Log("Victory");
        win_label.gameObject.SetActive(true);
        Enemy enemy = IEnemy.GetComponent<Enemy>();
        win_label.text = "Поздравляем! " + enemy.name + " повержен";
        resetGame.gameObject.SetActive(true);
    }
}