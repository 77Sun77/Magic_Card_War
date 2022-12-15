using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCard_3 : MonoBehaviour
{
    public GameObject explosion;

    void Update()
    {
        transform.Translate(Vector2.down * 2.5f * Time.deltaTime);
        if(transform.position.y <= 2)
        {
            Vector2 vec = transform.position;
            vec.y = 2f;
            Instantiate(explosion, vec, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
