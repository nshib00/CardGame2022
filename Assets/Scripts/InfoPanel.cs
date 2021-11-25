using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Text Heart;
    public Text LightMana;
    public Text Materia;
    public Text DarkMana;
    public GameObject PlayerGameObject;

    private Player PlayerInfo;

    private void Start()
    {
        PlayerInfo = PlayerGameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        Heart.text = PlayerInfo.PlayerHealth.ToString();
        LightMana.text = PlayerInfo.Mana.Light.ToString();
        Materia.text = PlayerInfo.Mana.Materia.ToString();
        DarkMana.text = PlayerInfo.Mana.Dark.ToString();
    }
}
