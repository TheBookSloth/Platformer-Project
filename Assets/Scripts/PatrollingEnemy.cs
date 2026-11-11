using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    public Transform pointLeft;
    public Transform pointRight;
    public float speed = 5f;
    public bool takingDamage = false;
    public bool isDead = false;

    private Transform currentPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentPoint = pointRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (takingDamage == false){
            if (currentPoint == pointRight) rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            else rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if (isDead) Destroy(gameObject);

        if(Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f)
        {
            if (currentPoint == pointRight)
            {
                currentPoint = pointLeft;
                sprite.flipX = true;
            }
            else
            {
                currentPoint = pointRight;
                sprite.flipX = false;
            }
        }
    }

    public void GetHit()
    {
        animator.Play("Enemy Death");
    }
}
