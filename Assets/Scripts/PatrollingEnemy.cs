using Unity.VisualScripting;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public Transform pointLeft;
    public Transform pointRight;
    public float speed = 5f;

    private Transform currentPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentPoint = pointRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPoint == pointRight) rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        else rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);

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
}
