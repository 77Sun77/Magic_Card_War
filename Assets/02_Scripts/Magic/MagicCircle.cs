using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    Animator anim;
    bool isActive, isDelayTime;
    int magicStack;
    float timer, delayTime;

    public List<GameObject> prefabs = new List<GameObject>();
    void Start()
    {
        anim = GetComponent<Animator>();
        isActive = false;
        delayTime = 0;
        magicStack = 0;
    }

    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                isActive = false;
                anim.SetTrigger("OffCircle");
            }

            delayTime -= Time.deltaTime;
            if(delayTime <= 0 && isDelayTime)
            {

                isDelayTime = false;
            }
        }
    }

    public void OnMagicCicle(GameObject prefab)
    {
        prefabs.Add(prefab);
        if (!isActive)
        {
            timer = 1.5f;
            if (magicStack == 0) delayTime = 0.5f;
            magicStack++;
            magicStack = 0;
            isDelayTime = true;
            isActive = true;
            anim.SetTrigger("OnCircle");
        }
        else
        {
            OnMagic();
        }
    }

    public void OnMagic()
    {
        if (isActive)
        {
            timer = 1.5f;

        }
        GameObject prefab = Instantiate(prefabs[0]);
        Empowerment(prefab);
        prefabs.RemoveAt(0);

    }

    // 카드 능력 부여
    void Empowerment(GameObject prefab)
    {
        if(prefab.GetComponent<Projectile>() != null) // 투사체라면
        {
            if (prefabs[0].name == "FireBall") prefab.GetComponent<Projectile>().Setting(2, 3.5f);
            if (prefabs[0].name == "FireBall2") prefab.GetComponent<Projectile>().Setting(3, 5f);
        }
    }
}
