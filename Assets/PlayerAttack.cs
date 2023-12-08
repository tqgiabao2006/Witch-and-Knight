using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   
 
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public float damge;
    public Animator anim;

    public AudioSource slash;
    private void Start()
    {
        
        anim = GetComponent<Animator>();   
    }
    private void Update()
    {   
       
        
           
            if (Input.GetMouseButtonDown(0))
            {
           
                anim.SetTrigger("isAttack");
                Collider2D[] enemiesToDamge = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamge.Length; i++)
                {
                    enemiesToDamge[i].GetComponent<BossHealth>().TakeDamage(damge);
                    
                }
            }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    public void Slash()
    {
      slash.Play();
    }
}
