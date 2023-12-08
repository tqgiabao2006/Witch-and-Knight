using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 500f;
    Animator animator;
    BosshealthUI BosshealthUI;
    public void Start()
    {
        BosshealthUI = GameObject.FindGameObjectWithTag("Boss Health").GetComponent<BosshealthUI>();
        animator = GetComponent<Animator>();
        health = 500f;
        BosshealthUI.MaxValue(health);
    }
    public void TakeDamage(float damage)
    {  
        health -= damage;
        BosshealthUI.SetHealth(health);
        animator.SetTrigger("isHit");
        if (health <= 0)
        {
            animator.SetTrigger("Die");
         
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
   
}
