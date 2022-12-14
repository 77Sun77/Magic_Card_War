using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> Card_List = new List<GameObject>();
    public List<GameObject> None_List = new List<GameObject>();

    public Transform LayoutGroup, CardList;
    public GameObject None;
    public List<GameObject> CardPrefab = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            CardPrefab.Add((GameObject)Resources.Load("Prefabs/Card/Fire/Fire " + (i + 1)));
            CardPrefab.Add((GameObject)Resources.Load("Prefabs/Card/Water/Water " + (i + 1)));
            CardPrefab.Add((GameObject)Resources.Load("Prefabs/Card/Ground/Ground " + (i + 1)));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) CardSpawn();
        for(int i=0;i< None_List.Count; i++)
        {
            None_List[i].transform.localPosition = new Vector2(800 - (200 * i), -360f);
        }
        
    }

    public void CardSpawn()
    {
        GameObject none = Instantiate(None, LayoutGroup);
        None_List.Add(none);
        int random = Random.Range(0, 3);
        GameObject card = Instantiate(CardPrefab[random], CardList);
        card.transform.localPosition = new Vector2(-1500, -360f); 
        card.GetComponent<Card>().Setting(none.transform, true);
        foreach (GameObject GO in Card_List) GO.GetComponent<Card>().Setting(true);
        Card_List.Add(card);
        
    }
    public void CardSpawn(string name, Transform go,Transform none)
    {
        GameObject card = new GameObject();
        foreach (GameObject prefab in CardPrefab)
        {
            if (prefab.name == name)
            {
                card = Instantiate(prefab, CardList);
                break;
            }
        }
        card.transform.localPosition = go.localPosition;
        card.GetComponent<Card>().Setting(none.transform, true);
        foreach (GameObject GO in Card_List) GO.GetComponent<Card>().Setting(true);
        Card_List.Add(card);
    }

    public void Card_Reset(GameObject card) // 카드 합성
    {
        Card_List.Remove(card);
        Destroy(card);
        foreach (GameObject GO in Card_List) GO.GetComponent<Card>().Setting(true);
    }
    public void Card_Reset(GameObject card, GameObject none) // 카드 사용
    {
        Card_List.Remove(card);
        None_List.Remove(none);
        Destroy(card);
        Destroy(none);
        foreach (GameObject GO in Card_List) GO.GetComponent<Card>().Setting(true);
    }
}
