using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;

    public void TakeDamge(int damge)
    {
        health -= damge;
        
    }
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
