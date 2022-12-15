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

    public GameObject[] monsters;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        GameObject deleteGO = GameObject.Find("New Game Object");
        if (deleteGO != null) Destroy(deleteGO);

        Sort();
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

    void Sort() // 몬스터 정렬
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = 0; i < monsters.Length - 1; i++)
        {
            int index = i;
            for (int j = i + 1; j < monsters.Length; j++)
            {
                float x1 = monsters[i].transform.position.x;
                float x2 = monsters[j].transform.position.x;
                if (x1 > x2) index = j;
            }

            if (index != i)
            {
                GameObject temp = monsters[index];
                monsters[index] = monsters[i];
                monsters[i] = temp;
            }
        }
        this.monsters = monsters;
    }
}
