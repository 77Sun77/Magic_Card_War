using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void Setting(int damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

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
            // 데미지를 통해 hp 깎음
        }
    }
}
