using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public float playerhealth = 100f;
    public float currentHealth;
    public healthValue hb;
    public AudioSource slash11;
    public AudioSource slash12;
    private void Start()
    {
        hb = GameObject.FindGameObjectWithTag("Health").GetComponent<healthValue>();
        currentHealth = playerhealth;
        hb.MaxValue(playerhealth);
        
    }
    private void Update()
    {
        if(currentHealth <=0 )
        {
            Die(); 
        }
    }
    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
         hb.SetHealth(currentHealth);
    }
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
           
            TakeDamage(attackDamage);
        }
    }   

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
    public void Attack11()
    {
        slash11.Play();
    }
    public void Attack12()
    {
        slash12.Play();
    }
}

