using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public void Setting(bool isMove)
    {
        this.isMove = isMove;
    }
    public void Setting(Transform target, bool isMove)
    {
        this.target = target;
        this.isMove = isMove;
    }

    public Transform target;
    public bool isMove, isDrag;

    Vector2 mousePos;

    public string kind;
    public int rank;

    string nextCard;
    void Start()
    {
        isMove = true;
        isDrag = false;

        for (int i = 0; i < gameObject.name.Length; i++)
        {
            if ((gameObject.name.Substring(0, i)).Contains(" "))
            {
                kind = gameObject.name.Substring(0, i - 1);
                break;
            }
        }
        if (gameObject.name.Contains("1")) rank = 1;
        else if (gameObject.name.Contains("2")) rank = 2;
        else if (gameObject.name.Contains("3")) rank = 3;
    }

    void Update()
    {
        if (isMove) Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Image image = GetComponent<Image>();
            Color color = image.color;
            color.a = 255 *0.5f;
            image.color = color;
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 4000f * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            isMove = false;
            transform.position = target.position;

        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
        {
            mousePos = new Vector2(eventData.position.x, eventData.position.y + 200);
            transform.position = mousePos;

            if (transform.localPosition.y >= 30)
            {
                GameManager.instance.Blind[0].SetActive(false);
            }
            else
            {
                GameManager.instance.Blind[0].SetActive(true);
            }
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isMove)
        {
            isDrag = true;
            mousePos = new Vector2(eventData.position.x, eventData.position.y + 200);
            transform.position = mousePos;
            transform.localScale = transform.localScale * 1.2f;
            GetComponent<RectTransform>().SetAsLastSibling();
            GameManager.instance.On_Blind();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDrag)
        {
            if (transform.localPosition.y >= 30)
            {

                Card_Destroy();
            }
            else if (isMerge && rank < 3)
            {
                Card_Destroy(nextCard, merge.transform, merge.GetComponent<Card>().target);
            }
            merge = null;
            transform.localPosition = target.localPosition;
            transform.localScale = transform.localScale / 1.2f;
            isDrag = false;

            GameManager.instance.Off_Blind();
            triggerCard.RemoveRange(0, triggerCard.Count);
        }
    }

    void Card_Destroy(string name, Transform go, Transform target)
    {
        SpawnManager manager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (manager.Card_List.IndexOf(this.target.gameObject) < manager.Card_List.IndexOf(target.gameObject))
        {
            manager.CardSpawn(name, go, this.target);
            manager.Card_Reset(gameObject);
            manager.Card_Reset(merge, target.gameObject);
        }
        else
        {
            manager.CardSpawn(name, go, target);
            manager.Card_Reset(gameObject);
            manager.Card_Reset(merge, this.target.gameObject);
        }
    }
    void Card_Destroy()
    {
        SpawnManager manager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        manager.Card_Reset(gameObject, target.gameObject);
    }

    GameObject merge;
    bool isMerge;
    List<GameObject> triggerCard = new List<GameObject>();

    public void OnTriggerEnter2D(Collider2D GO)
    {
        if (GO.tag == "Card") triggerCard.Add(GO.gameObject);
    }
    public void OnTriggerStay2D(Collider2D GO)
    {
        if (GO.tag == "Card" && isDrag)
        {
            Card card = GO.GetComponent<Card>();
            if (card.kind == kind && card.rank == rank)
            {
                float distance = 1000;
                foreach (GameObject go in triggerCard)
                {
                    if(go != null)
                    {
                        if (Vector2.Distance(go.transform.position, transform.position) < distance)
                        {
                            if (go != merge && merge != null)
                            {
                                merge.transform.Find("Blind").gameObject.SetActive(true);
                            }
                            distance = Vector2.Distance(go.transform.position, transform.position);
                            
                            merge = go.gameObject;
                            merge.transform.Find("Blind").gameObject.SetActive(false);
                        }
                    }
                    
                }
                isMerge = true;
                nextCard = kind + " " + (rank + 1);
            }

        }
    }

    public void OnTriggerExit2D(Collider2D GO)
    {
        if (GO.tag == "Card" && GO.gameObject == merge)
        {
            print("GG");
            triggerCard.Remove(GO.gameObject);
            isMerge = false;
            nextCard = "";
            merge.transform.Find("Blind").gameObject.SetActive(true);
        }
    }
}
