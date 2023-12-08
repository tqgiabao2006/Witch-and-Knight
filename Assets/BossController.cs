using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    SpriteRenderer rb;

    private void Start()
    {
        rb= GetComponent<SpriteRenderer>();
    }
    //private void Update() 
   // {
        //LookAtPlayer();
   // }
    public void LookAtPlayer()
    {


        if (this.transform.position.x > player.position.x)
        {
            rb.flipX = true;
        }
        else if (this.transform.position.x < player.position.x)
        {
            rb.flipX= false;
        }

    }

}  
