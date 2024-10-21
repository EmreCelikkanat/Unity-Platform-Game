using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 7;
    [SerializeField]
    private float jumpForce = 5;

    private float moveInput;
    private bool facingRight = true;
    private bool isGrounded;

    public Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    public float checkRadius = 0.1f;
    public LayerMask whatIsGround;
   
    [SerializeField] private LayerMask wallLayer;

    private bool isWallJumping;
    private Vector2 wallJumpingPower = new Vector2(6f, 12f); 
    private float wallJumpingDirection;
    private float wallJumpingTime=0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration=0.4f;

    private int extraJump;
    public int extraJumpValue = 2;

    private bool isWallsliding;
    private float wallSlindingSpeed = 1f;

    private Animator Anim;
    private Rigidbody2D rb;

    [SerializeField] private float coyoteTime=0.2f;
    private float coyoteCounter;

    [SerializeField] private AudioClip jumpSound;


    void Start()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        extraJump = extraJumpValue;
        rb.gravityScale = 5;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            Jump();
        if (Input.GetKeyUp(KeyCode.X) && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
            extraJump = extraJumpValue;
        }
        else
            coyoteCounter -= Time.deltaTime;
   

        Anim.SetBool("run", moveInput != 0);
        Anim.SetBool("ground", isGrounded);

        WallSlide();
        WallJumping();
    }
    private void FixedUpdate()
    {

        isGrounded=Physics2D.OverlapCircle(groundCheck.position, checkRadius,whatIsGround);
        moveInput = Input.GetAxis("Horizontal");
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }
       
    }
    void Flip()
    {
        facingRight=!facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void Jump()
    {
        if (coyoteCounter <= 0 && !isWall() && extraJump <= 0) return;
        SoundManager.instance.PlaySound(jumpSound);
        if (isWall())
            WallJumping();
        else
        {
            if (isGrounded)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            else
            {
                if (coyoteCounter > 0)
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                else
                {
                    if (extraJump > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        extraJump--;
                    }
                }
            }
            coyoteCounter = 0;
        }
    }
    private void WallSlide()
    {
        if(!isGrounded && isWall() && moveInput!= 0f) 
        {
            isWallsliding = true;
            rb.velocity=new Vector2(rb.velocity.x,Mathf.Clamp(rb.velocity.y,-wallSlindingSpeed,float.MaxValue));
        }
        else
            isWallsliding=false;
    }

    private void WallJumping()
    {
        if (isWallsliding)
        {
            isWallJumping = false;
            wallJumpingDirection=-transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter-=Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.X) && wallJumpingCounter > 0f)
        {
            isWallJumping=true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter=0;
            if (transform.localScale.x != wallJumpingDirection)
            {
                Flip();
            }
            Invoke(nameof(StopWallJumping),wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }
    private bool isWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    public bool CanAttack()
    {
        return moveInput == 0 && isGrounded && !isWall();
    }
}
