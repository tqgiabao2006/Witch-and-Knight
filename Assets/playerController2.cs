using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController2 : MonoBehaviour
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

    private Animator anim;
    public AudioSource runEffect;
  
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        direction = Input.GetAxisRaw("Horizontal");


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

        if(isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        if (isGrounded == true)
        {
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    runEffect.Play();
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {   
                rb.velocity = Vector2.up * jumpforce;
                doubleJump = true;
                anim.SetBool("isJumping", true);

            }
            else if (doubleJump)
            {
                anim.SetBool("isJumping", true);
                rb.velocity = Vector2.up * jumpforce;
                doubleJump = false;

            }
        }



        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpCounter > 0 && isJumping == true)
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
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float orginalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(direction * dashingPower, 0f);
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
       }
    }
