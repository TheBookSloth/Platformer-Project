using System;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float knockbackForce = 4f;
    public float moveDir = 0;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool isGrounded = false;
    private bool gettingKnockback = false;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private float knockbackTimer = 0f;

    private Vector3 initialPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        initialPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        //Interpret Inputs
        if (Input.GetKey(KeyCode.A)) moveDir = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir = 1f;
        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
            moveDir = 0;

        //Move Player
        if (!gettingKnockback) rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);
        else knockbackTimer += 1 * Time.deltaTime;

        //Change Animator Variables for idle, walking, and jumping
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("IsJumping", !isGrounded);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        //Has the Player look in the correct direction
        if (moveDir < 0) sprite.flipX = true;
        else if (moveDir > 0) sprite.flipX = false;

        if (transform.position.y < -6.5f)
        {
            rb.linearVelocity = new Vector2(0, 0);
            transform.position = initialPos;
        }
    }

    private void FixedUpdate()
    {
        
        if (knockbackTimer >= 0.125f || knockbackTimer == 0f) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) gettingKnockback = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {

        }
        else if (collision.gameObject.CompareTag("Enemy") && isGrounded)
        {
            gettingKnockback = true;
            isGrounded = false;
            knockbackTimer = 0.01f;
            if (collision.transform.position.x < transform.position.x)
            {
                rb.linearVelocity = new Vector2(knockbackForce, knockbackForce);
            }
            else
            {
                rb.linearVelocity = new Vector2(-knockbackForce, knockbackForce);
            }
        }
    }

    
}
