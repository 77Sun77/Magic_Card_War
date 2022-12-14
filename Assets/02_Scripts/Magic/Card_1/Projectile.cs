using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour // 구체와 관통형에만 적용
{
    public void Setting(int damage, float speed) 
    {
        this.damage = damage;
        this.speed = speed;
    }

    public enum Kind { Sphere, Penetrate };
    public Kind MagicKind;

    int damage;
    float speed;

    void Start()
    {

    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    public void OnTriggerEnter2D(Collider2D GO)
    {
        if(GO.tag == "Monster")
        {
            Monster m = GO.GetComponent<Monster>();
            m.Take_Damage(damage);
            if(MagicKind == Kind.Sphere)
            {
                Destroy(gameObject);
            }
        }
    }
}
