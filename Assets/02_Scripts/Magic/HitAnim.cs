using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnim : MonoBehaviour
{
    Projectile.Kind kind;
    float damage;
    bool isAttack;
    
    public void Set_Extent(float damage)
    {
        kind = Projectile.Kind.Extent;
        this.damage = damage;
        isAttack = true;
    }
    public void AnimDestroy() 
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttack && collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().Take_Damage(damage);
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(0.2f);
        isAttack = false;
    }
}
