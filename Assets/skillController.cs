using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float lifetime = 1;
    public float damge = 10f;
    BossHealth health;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Destroy(this.gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        BossHealth hb = other.GetComponent<BossHealth>();
        if (other.gameObject.CompareTag("Enemy"))
        {
            hb.TakeDamage(damge);
            Destroy(this.gameObject);
        }
        
    }

}
   
