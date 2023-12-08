using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillR : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float lifetime = 4;
    public float damge = 50f;
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
