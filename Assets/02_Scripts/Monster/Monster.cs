using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum Kind { Normal };
    public Kind MonsterKind;

    int hp, damage;
    float speed;


    void Start()
    {
        if(MonsterKind == Kind.Normal)
        {
            Set_Monster(5, 3, 0.7f);
        }
    }

    void Update()
    {

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void Set_Monster(int hp, int damage, float speed)
    {
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
    }

    public void Take_Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
