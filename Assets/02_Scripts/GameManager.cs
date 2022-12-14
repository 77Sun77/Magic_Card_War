using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Script")]
    public SpawnManager SpawnManager;
    public MagicCircle magicCircle;

    [Header("UI")]
    public GameObject Blind_Parent;
    public GameObject[] Blind;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void On_Blind()
    {
        Blind_Parent.SetActive(true);
        foreach (GameObject card in SpawnManager.Card_List) card.transform.Find("Blind").gameObject.SetActive(true);
        foreach (GameObject blind in Blind) blind.SetActive(true);

    }

    public void Off_Blind()
    {
        Blind_Parent.SetActive(false);
        foreach (GameObject card in SpawnManager.Card_List) card.transform.Find("Blind").gameObject.SetActive(false);
        foreach (GameObject blind in Blind) blind.SetActive(false);
    }
}
