using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float moveDir = 0;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool isGrounded = false;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
        rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);

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
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
