using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class _playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpforce;
    private float jumpCounter;
    public float jumpTime;
    private bool isJumping;
    private bool doubleJump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    [SerializeField] TrailRenderer tr;
    bool facingRight = true;
    private float direction;
    private bool isCharging = false;
    private Animator anim;


    [SerializeField] float mana;
    [SerializeField] float ultimateskillMana;
    [SerializeField] float skillMana;
    [SerializeField] float norMana;


    //-------------------------------//
    [SerializeField] GameObject ultimate;
    [SerializeField] GameObject norAttack;
    [SerializeField] GameObject skillAttack;
    [SerializeField] Transform skillPoint;
    public float _speedBullet = 10f;
    manaValue manaValue;
 
    public AudioSource charging;
    public AudioSource nor;
    public AudioSource spe;
          public AudioSource ultimate1;
    private void Start()
    {
        manaValue = GameObject.FindGameObjectWithTag("Mana").GetComponent<manaValue>();
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mana = 0;
        ultimateskillMana = 2;
        skillMana = 1;
        norMana = 0.2f;
        manaValue.Current(mana);
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
         direction = Input.GetAxisRaw("Horizontal");
        if(isCharging==false)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            if (direction < 0 && facingRight)
            {
                Flip();
            }
            if (direction > 0 && !facingRight)
            {
                Flip();
            }
        }
        

    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void Update()
    {
        if (isDashing)
        {
            return;
        }
    
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isCharging == false) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    rb.velocity = Vector2.up * jumpforce;
                    doubleJump = true;

                }
                else if (doubleJump)
                {
                    rb.velocity = Vector2.up * jumpforce;
                    doubleJump = false;

                }
            }
        }


        if (Input.GetKey(KeyCode.Space))
        { if (jumpCounter > 0 && isJumping == true)
            {
                rb.velocity = Vector2.up * jumpforce;
                jumpCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());

        }
        AnimController();
        if (mana>= norMana && Input.GetMouseButtonDown(0))
        {   
            mana -= norMana;
            manaValue.SetMana(mana);
            nor.Play();
            ShootingNor();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (mana<= 4)
            {
                
                mana += Time.deltaTime;
                manaValue.SetMana(mana);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            charging.Play();

        }else if(Input.GetKeyUp(KeyCode.Q))
        {
            charging.Stop();
        }
        if (mana >= ultimateskillMana && Input.GetKeyDown(KeyCode.R))
        {

            mana -= ultimateskillMana;
            manaValue.SetMana(mana);
            ShootingUltimate();
            ultimate1.Play();
        }
        if(mana >= skillMana && Input.GetKeyDown(KeyCode.E))
        {   spe.Play();
            mana -= skillMana;
            manaValue.SetMana(mana);
            ShootingSKill();

        }

      
       

    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float orginalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(direction * dashingPower,0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = orginalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private void AnimController()
    {
        if (direction == 0)
        {
            anim.SetBool("isRunning", false);

        }
        else
        {
            anim.SetBool("isRunning", true);
        }


        if (Input.GetKey(KeyCode.Q))
        {
            isCharging = true;
            anim.SetBool("isCharging", true);
        }
        else
        {
            isCharging = false;
            anim.SetBool("isCharging", false);
        }

        
    }
    private void ShootingUltimate()
    {
        var bullet = Instantiate(ultimate, skillPoint.position,skillPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = skillPoint.right * _speedBullet;  

    }
    private void ShootingSKill()
    {
        var bullet = Instantiate(skillAttack, skillPoint.position, skillPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = skillPoint.right * _speedBullet;

    }
    private void ShootingNor()
    {
        var bullet = Instantiate(norAttack, skillPoint.position, skillPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = skillPoint.right * _speedBullet;

    }
}
