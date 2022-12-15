using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Explosion : MonoBehaviour
{
    int count;
    void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(count < 5 && coll.CompareTag("Monster"))
        {
            Monster m = coll.GetComponent<Monster>();
            m.Take_Damage(5);
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
